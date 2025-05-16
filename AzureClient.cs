using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Vano.Tools.Azure.Model;

namespace Vano.Tools.Azure
{
    public class AzureClient : IAzureClient
    {
        #region Private members

        private bool _initialized = false;
        private HttpClient _client;
        private SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private AuthenticationContext _authContext;
        private TokenCache _cache = new TokenCache();

        #endregion

        #region Constants

        // Azure Stack PowerShell client id
        // private const string AppClientId = "0a7bdc5c-7b57-40be-9939-d4c5fc7cd417";

        // Azure PowerShell client id
        private const string AppClientId = "1950a258-227b-4e31-a9cf-717495945fc2";

        private const string AppRedirectUri = "urn:ietf:wg:oauth:2.0:oob";

        /// <summary>
        /// Storage account name must be between 3 and 24 characters in length and use numbers and lower-case letters only.
        /// </summary>
        public static readonly Regex StorageNameValidation = new Regex(@"^[a-z0-9]{3,24}$", RegexOptions.Compiled);

        /// <summary>
        /// Resource group name can only include alphanumeric characters, periods, underscores, hyphens and parenthesis and cannot end in a period. Length (1,64).
        /// </summary>
        public static readonly Regex ResourceGroupValidation = new Regex(@"^[-_a-z0-9()\.]{0,63}[-_a-z0-9()]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Constructors

        public AzureClient(
            string resourceManagerEndpoint = "management.azure.com",
            string apiVersion = "2015-01-01",
            AzureMetadata metadata = null,
            Func<HttpMessageHandler> handlerFactory = null)
        {
            this.ResouceManagerEndpoint = resourceManagerEndpoint;
            this.ApiVersion = apiVersion;
            this.Metadata = metadata;

            _client = CreateHttpClient(handlerFactory);
        }

        public AzureClient(
            string resourceManagerEndpoint = "management.azure.com",
            string apiVersion = "2015-01-01",
            string authenticationEndpoint = "https://login.windows.net",
            string appResourceId = "https://management.core.windows.net/",
            Func<HttpMessageHandler> handlerFactory = null)
        {
            this.ResouceManagerEndpoint = resourceManagerEndpoint;
            this.ApiVersion = apiVersion;
            this.AuthenticationEndpoint = authenticationEndpoint;
            this.AppResourceId = appResourceId;

            _client = CreateHttpClient(handlerFactory);
        }

        #endregion

        #region Public properties

        public AzureMetadata Metadata { get; private set; }

        public string ResouceManagerEndpoint { get; private set; }

        public string ApiVersion { get; private set; }

        public string AuthenticationEndpoint { get; private set; }

        public string AppResourceId { get; private set; }

        public HttpHeadersProcessor HttpHeadersProcessor { get; set; }

        #endregion

        #region Public Methods - Initialize

        public async Task Initialize()
        {
            if (!_initialized)
            {
                await _lock.WaitAsync();
                try
                {
                    if (!_initialized)
                    {
                        await InitializeInternal();
                        _initialized = true;
                    }
                }
                finally
                {
                    _lock.Release();
                }
            }
        }

        private async Task InitializeInternal()
        {
            if (string.IsNullOrEmpty(this.AuthenticationEndpoint) || string.IsNullOrEmpty(this.AppResourceId))
            {
                if (this.Metadata == null)
                {
                    // dogfood environment uses a different api version for the metadata endpoint.
                    this.Metadata = await GetAzureMetadata(
                        this.ResouceManagerEndpoint,
                        apiVersion: this.ResouceManagerEndpoint.Contains("dogfood") ? "2014-11-01-privatepreview" : "1.0");
                }

                //BUGBUG: The dogfood's metadata endpoint doesn't retieve correct login endpoint for the dogfood's environemnt
                this.AuthenticationEndpoint = this.ResouceManagerEndpoint.Contains("dogfood") ? "https://login.windows-ppe.net" : this.Metadata.LoginEndpoint;
                this.AppResourceId = this.Metadata.Audiences.FirstOrDefault();
            }

            // Also clear cookies from the browser control.
            ClearCookies();

            Uri authenticationUri = new Uri(this.AuthenticationEndpoint);
            Uri commonAuthority = authenticationUri.LocalPath == "/" ? new Uri(authenticationUri, "common") : authenticationUri;

            _authContext = new AuthenticationContext(
                authority: commonAuthority.ToString(),
                validateAuthority: false,
                tokenCache: _cache);

            AuthenticationResult result = null;
            try
            {
                result = await _authContext.AcquireTokenAsync(
                    resource: this.AppResourceId,
                    clientId: AppClientId,
                    redirectUri: new Uri(AppRedirectUri),
                    parameters: new PlatformParameters(PromptBehavior.SelectAccount));

                Trace.WriteLine("Authority: " + _authContext.Authority);
            }
            catch (AdalException ex)
            {
                if (ex.ErrorCode == "authentication_canceled")
                {
                    Trace.TraceInformation(ex.Message);

                    throw new OperationCanceledException(ex.Message, ex);
                }

                Trace.TraceError(ex.ToString());

                throw;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());

                throw;
            }
        }

        #endregion

        #region Public Methods - ARM Operations

        public async Task<IEnumerable<string>> GetTenantsIds(CancellationToken cancellationToken = new CancellationToken())
        {
            AuthenticationResult token = await GetToken();

            JObject response = await CallAzureResourceManagerAsJObject("GET", "/tenants", token, cancellationToken: cancellationToken);
            IEnumerable<string> tenantIds = response
                .Value<JArray>("value")
                .Select(tenant => tenant.Value<string>("tenantId"));

            foreach (string tenantId in tenantIds)
            {
                Trace.WriteLine("Tenant: " + tenantId);
            }

            return tenantIds;
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptions(CancellationToken cancellationToken = new CancellationToken())
        {
            await Initialize();

            List<Subscription> subscriptions = new List<Subscription>();

            IEnumerable<string> tenantsIds = await GetTenantsIds();
            foreach (string tenantId in tenantsIds)
            {
                IEnumerable<Subscription> subscriptionsInTenant = null;
                try
                {
                    AuthenticationResult tenantToken = await GetTenantToken(tenantId);

                    if (tenantToken == null)
                    {
                        continue;
                    }

                    JObject response = await CallAzureResourceManagerAsJObject("GET", "/subscriptions", tenantToken, cancellationToken: cancellationToken);

                    subscriptionsInTenant = response
                        .Value<JArray>("value")
                        .Select(tenant => new Subscription()
                        {
                            Id = tenant.Value<string>("subscriptionId"),
                            DisplayName = tenant.Value<string>("displayName"),
                            State = tenant.Value<string>("state"),
                            TenantId = tenantId
                        });
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.ToString());

                    // AADSTS50001: The application named https://<app>.<directory>.onmicrosoft.com was not found in the tenant named <tenantid>.
                    if (e.HResult != -2146233088)
                    {
                        throw;
                    }
                }

                if (subscriptionsInTenant != null)
                {
                    subscriptions.AddRange(subscriptionsInTenant);
                }
            }

            return subscriptions.OrderBy(sub => sub.DisplayName);
        }

        public async Task<IEnumerable<Location>> GetLocations(Subscription subscription, CancellationToken cancellationToken = new CancellationToken())
        {
            AuthenticationResult tenantToken = await GetTenantToken(subscription.TenantId);

            JObject response = await CallAzureResourceManagerAsJObject("GET", string.Format(@"/subscriptions/{0}/locations", subscription.Id), tenantToken, cancellationToken: cancellationToken);

            IEnumerable<Location> locations = response.Value<JArray>("value").ToObject<IEnumerable<Location>>();

            return locations;
        }

        #endregion

        #region Public Static Methods - Metadata

        public static async Task<AzureMetadata> GetAzureMetadata(string azureResourceManager = "management.azure.com", string apiVersion = "1.0")
        {
            JObject response = await GetAzureResourceManagerMetadataAsJObject(azureResourceManager, apiVersion);
            JObject authentication = response.Value<JObject>("authentication");

            AzureMetadata metadata = new AzureMetadata()
            {
                Audiences = authentication
                    .Value<JArray>("audiences")
                    .Select(audience => audience.Value<string>())
                    .ToArray(),
                LoginEndpoint = authentication
                    .Value<string>("loginEndpoint")
            };

            return metadata;
        }

        private static async Task<JObject> GetAzureResourceManagerMetadataAsJObject(string azureResourceManager, string apiVersion)
        {
            string response = await GetAzureResourceManagerMetadata(azureResourceManager, apiVersion);
            if (!string.IsNullOrWhiteSpace(response))
            {
                return JObject.Parse(response);
            }

            return new JObject();
        }

        private static async Task<string> GetAzureResourceManagerMetadata(string azureResourceManager, string apiVersion)
        {
            string azureResourceManagerMetadataEndpoint = string.Format("https://{0}/metadata/endpoints?api-version={1}", azureResourceManager, apiVersion);

            Trace.WriteLine("GET " + azureResourceManagerMetadataEndpoint);

            Uri requestUri = new Uri(azureResourceManagerMetadataEndpoint);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = "GET";
            request.ContentLength = 0;

            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream receiveStream = response.GetResponseStream())
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    string result = await readStream.ReadToEndAsync();

                    Trace.WriteLine(result);

                    return result;
                }
            }
        }

        #endregion

        #region Private Methods - Tokens

        public async Task<string> GetAuthSecret(string tenantId = null)
        {
            AuthenticationResult result = tenantId == null ?
                await GetToken() :
                await GetTenantToken(tenantId);

            return result.CreateAuthorizationHeader();
        }

        private async Task<AuthenticationResult> GetToken()
        {
            AuthenticationResult result = null;
            try
            {
                result = await _authContext.AcquireTokenAsync(
                    resource: this.AppResourceId,
                    clientId: AppClientId,
                    redirectUri: new Uri(AppRedirectUri),
                    parameters: new PlatformParameters(PromptBehavior.Never));
            }
            catch (AdalException ex)
            {
                Trace.TraceError(ex.ToString());

                throw;
            }

            return result;
        }

        private async Task<AuthenticationResult> GetTenantToken(string tenantId)
        {
            AuthenticationResult token = await GetToken();

            Uri authenticationUri = new Uri(this.AuthenticationEndpoint);
            Uri tenantAuthority =
                authenticationUri.LocalPath == "/" ?
                new Uri(authenticationUri, tenantId) :
                new Uri(authenticationUri, authenticationUri.PathAndQuery + "/" + tenantId);

            AuthenticationContext tenantAuthContext = new AuthenticationContext(
                authority: tenantAuthority.ToString(),
                validateAuthority: false,
                tokenCache: _cache);

            AuthenticationResult result = null;
            try
            {
                result = await tenantAuthContext.AcquireTokenAsync(
                    resource: this.AppResourceId,
                    clientId: AppClientId,
                    redirectUri: new Uri(AppRedirectUri),
                    parameters: new PlatformParameters(PromptBehavior.Auto),
                    userId: new UserIdentifier(token.UserInfo.DisplayableId, UserIdentifierType.OptionalDisplayableId));
            }
            catch (Exception ex)
            {
                Trace.TraceError($"TenantId: {tenantId}. Error: {ex.Message}");

                return null;
            }

            return result;
        }

        #endregion

        #region Private Methods - ARM Helper Methods

        private static HttpClient CreateHttpClient(Func<HttpMessageHandler> handlerFactory = null)
        {
            HttpClient client = handlerFactory != null ?
                new HttpClient(handlerFactory()) :
                new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!client.DefaultRequestHeaders.Contains("User-Agent"))
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Visual ARM client");
            }

            return client;
        }

        private Uri CreateAzureResourceManagerUri(string path, Dictionary<string, string> parameters = null, string armEndpoint = null, string apiVersion = null)
        {
            armEndpoint = string.IsNullOrEmpty(armEndpoint) ? this.ResouceManagerEndpoint : armEndpoint;

            if (path.Contains("api-version="))
            {
                return new Uri(string.Format("https://{0}{1}{2}",
                    armEndpoint,
                    path,
                    parameters != null ?
                        string.Concat("&", string.Join("&", parameters.Select(p => string.Concat(p.Key, "=", p.Value)))) :
                        string.Empty)
                    .Replace(" ", "%20"));
            }

            return new Uri(string.Format("https://{0}{1}?api-version={2}{3}",
                armEndpoint,
                path,
                apiVersion ?? this.ApiVersion,
                parameters != null ?
                    string.Concat("&", string.Join("&", parameters.Select(p => string.Concat(p.Key, "=", p.Value)))) :
                    string.Empty)
                .Replace(" ", "%20"));
        }

        private async Task<JObject> CallAzureResourceManagerAsJObject(string method, string path, AuthenticationResult token, string body = null, Dictionary<string, string> parameters = null, string armEndpoint = null, string apiVersion = null, bool displaySecrets = false, CancellationToken cancellationToken = new CancellationToken())
        {
            return await CallAzureResourceManagerAsJObject(method, path, token != null ? token.CreateAuthorizationHeader() : null, body, parameters, armEndpoint, apiVersion, displaySecrets, cancellationToken);
        }

        private async Task<JObject> CallAzureResourceManagerAsJObject(string method, string path, string token, string body = null, Dictionary<string, string> parameters = null, string armEndpoint = null, string apiVersion = null, bool displaySecrets = false, CancellationToken cancellationToken = new CancellationToken())
        {
            string response = await CallAzureResourceManager(method, path, token, body, parameters, armEndpoint, apiVersion, displaySecrets, cancellationToken);
            if (!string.IsNullOrWhiteSpace(response))
            {
                return JObject.Parse(response);
            }

            return new JObject();
        }

        public async Task<string> CallAzureResourceManager(string method, string path, string token, string body = null, Dictionary<string, string> parameters = null, string armEndpoint = null, string apiVersion = null, bool displaySecrets = false, CancellationToken cancellationToken = new CancellationToken())
        {
            Uri requestUri = CreateAzureResourceManagerUri(path, parameters, armEndpoint, apiVersion);

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(method), requestUri);
            request.Headers.Add("Authorization", token);

            if (HttpHeadersProcessor != null)
            {
                HttpHeadersProcessor.CaptureHttpHeadersFromRequest(requestUri.Host, _client.DefaultRequestHeaders, displaySecrets);
                HttpHeadersProcessor.CaptureHttpHeadersFromRequest(requestUri.Host, request.Headers, displaySecrets);
            }

            if (!string.IsNullOrWhiteSpace(body))
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            using (HttpResponseMessage response = await _client.SendAsync(request, cancellationToken))
            {
                if (HttpHeadersProcessor != null)
                {
                    HttpHeadersProcessor.CaptureHttpHeadersFromResponse(response.StatusCode, response.Headers, displaySecrets);
                }

                string output = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(output);
                }

                return output;
            }
        }

        #endregion

        #region Private Static Methods - Clear Cookies

        private static void ClearCookies()
        {
            NativeMethods.InternetSetOption(IntPtr.Zero, NativeMethods.INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
        }

        private static class NativeMethods
        {
            internal const int INTERNET_OPTION_END_BROWSER_SESSION = 42;

            [DllImport("wininet.dll", SetLastError = true)]
            internal static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);
        }

        #endregion
    }
}
