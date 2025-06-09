using Azure.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Vano.Tools.Azure.Model;
using Vano.Tools.Azure.RDFE;

namespace Vano.Tools.Azure
{
    public class GeoMasterClient : IAzureClient
    {
        #region Private members

        private const string WebSystemName = "WebSites";
        private const long MaxReceivedMessageSize = 10 * 1024 * 1024;  // 10 MB
        private const int MaxStringContentLength = 1024 * 1024;        //  1 MB

        private HttpClient _client;

        #endregion

        #region Constants

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

        public GeoMasterClient(
            string geoMasterEndpoint,
            string apiVersion = "2015-01-01")
        {
            this.GeoMasterEndpoint = geoMasterEndpoint;
            this.ApiVersion = apiVersion;

            _client = CreateHttpClient();
        }

        #endregion

        #region Public properties

        public AzureMetadata Metadata 
        {
            get
            {
                // not used as we use CSM-Direct but to keep the interface happy.
                return new AzureMetadata()
                {
                    PortalEndpoint = "https://portal.azure.com",
                    Audiences = new [] { this.GeoMasterEndpoint },
                    GraphEndpoint = "https://graph.windows.net",
                    LoginEndpoint = "https://login.microsoftonline.com",
                };
            }
        }

        public string ResouceManagerEndpoint { get { return this.GeoMasterEndpoint; } }

        public string GeoMasterEndpoint { get; private set; }

        public string ApiVersion { get; private set; }

        public HttpHeadersProcessor HttpHeadersProcessor { get; set; }

        #endregion

        #region Public Methods - ARM Operations

        public async Task Initialize()
        {
            await GetAuthSecret();
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptions(CancellationToken cancellationToken = new CancellationToken())
        {
            List<Subscription> subscriptions = new List<Subscription>();

            SubscriptionClient rdfeClient = GetRdfeSubscriptionClient();            
            var rdfeSubscriptions = rdfeClient.GetSubscriptions(marker: "", recordCount: 100, ownerUserName: "");
            foreach(var rdfeSubscription in rdfeSubscriptions)
            {
                subscriptions.Add(new Subscription()
                {
                    Id = rdfeSubscription.Name,
                    DisplayName = string.IsNullOrWhiteSpace(rdfeSubscription.Description) ? "[CSM-Direct]" : rdfeSubscription.Description,
                    TenantId = "CsmDirect"
                });
            }

            // Make async call happy
            return await Task.FromResult(subscriptions.OrderBy(sub => sub.DisplayName));
        }

        public async Task<IEnumerable<Location>> GetLocations(Subscription subscription, CancellationToken cancellationToken = new CancellationToken())
        {
            List<Location> locations = new List<Location>();

            var tenantToken = await GetAuthSecret(subscription.TenantId);
            JObject response = await CallAzureResourceManagerAsJObject("GET", string.Format(@"/subscriptions/{0}/providers/Microsoft.Web/geoRegions", subscription.Id), tenantToken, cancellationToken: cancellationToken);

            foreach (var region in response["value"])
            {
                locations.Add(new Location()
                {
                    Id = region["id"]?.ToString(),
                    Name = region["name"]?.ToString(),
                    DisplayName = region["properties"]?["displayName"]?.ToString()
                });
            }

            return locations.OrderBy(sub => sub.DisplayName);
        }

        #endregion

        #region Private Methods - Tokens

        public async Task<string> GetAuthSecret(string tenantId = null)
        {
            AccessToken access = await DefaultAzureCredentialHelper.GetUserTokenAsync(UserCredentialExtensions.UserAADAuthParameter.AuthorityHost, UserCredentialExtensions.UserAADAuthParameter.TenantId, UserCredentialExtensions.UserAADAuthParameter.Scope);

            return access.Token;
        }

        #endregion

        #region Private Methods - ARM Helper Methods

        private HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            if (!client.DefaultRequestHeaders.Contains("User-Agent"))
            {
                client.DefaultRequestHeaders.Add("User-Agent", $"VisualARM/{Assembly.GetExecutingAssembly().GetName().Version}");
            }

            client.Timeout = TimeSpan.FromSeconds(30);

            return client;
        }

        private Uri CreateAzureResourceManagerUri(string path, Dictionary<string, string> parameters = null, string endpoint = null, string apiVersion = null)
        {
            string geoEndpoint = string.IsNullOrEmpty(endpoint) ? this.GeoMasterEndpoint : endpoint;
            if (path.Contains("api-version="))
            {
                return new Uri(string.Format("https://{0}{1}{2}",
                    geoEndpoint,
                    path,
                    parameters != null ?
                        string.Concat("&", string.Join("&", parameters.Select(p => string.Concat(p.Key, "=", p.Value)))) :
                        string.Empty)
                    .Replace(" ", "%20"));
            }

            return new Uri(string.Format("https://{0}{1}?api-version={2}{3}",
                geoEndpoint,
                path,
                apiVersion ?? this.ApiVersion,
                parameters != null ?
                    string.Concat("&", string.Join("&", parameters.Select(p => string.Concat(p.Key, "=", p.Value)))) :
                    string.Empty)
                .Replace(" ", "%20"));
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
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Simulate the CSM headers that are added by ARM.
            // http header "x-ms-client-authorization-source" to "legacy" if the caller is admin/co-admin and owner via RBAC.
            request.Headers.Add("x-ms-client-authorization-source", "legacy");

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
                    throw new AzureClientException(response.StatusCode, output);
                }

                return output;
            }
        }

        #endregion

        #region Private Methods - RDFE Helper Methods

        private SubscriptionClient GetRdfeSubscriptionClient()
        {
            ServiceEndpoint endpoint = GetRdfeServiceEndpoint(contractDescription: ContractDescription.GetContract(typeof(Vano.Tools.Azure.RDFE.Contracts.ISubscriptionService)), addressPostfix: UriElements.Subscriptions);

            SubscriptionClient client = new SubscriptionClient(endpoint);

            return client;
        }

        private ServiceEndpoint GetRdfeServiceEndpoint(ContractDescription contractDescription, string addressPostfix, bool supportsWebSystem = false)
        {
            // CSM          geomaster.joaquinvmss1.antares-test.windows-int.net:444
            // RDFE         geomaster.joaquinvmss1.antares-test.windows-int.net:443 GEO
            // RDFE         geomaster.joaquinvmss1.antares-test.windows-int.net:454 STAMP

            Uri csmEndpoint = new Uri("https://" + this.GeoMasterEndpoint);
            UriBuilder rdfeEndpoint = new UriBuilder("https", csmEndpoint.Host, portNumber: 443);

            string endpointAddress = rdfeEndpoint.ToString();
            if (!endpointAddress.EndsWith("/"))
            {
                endpointAddress += "/";
            }

            if (addressPostfix.StartsWith("/"))
            {
                addressPostfix = addressPostfix.Substring(1);
            }

            WebHttpBinding webHttpBinding = new WebHttpBinding();
            webHttpBinding.UseDefaultWebProxy = true;
            webHttpBinding.Security.Mode = WebHttpSecurityMode.Transport;
            webHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            webHttpBinding.MaxReceivedMessageSize = MaxReceivedMessageSize;
            webHttpBinding.ReaderQuotas.MaxStringContentLength = MaxStringContentLength;

            if (supportsWebSystem && !String.IsNullOrEmpty(WebSystemName))
            {
                addressPostfix = String.Format("{0}/{1}/{2}", UriElements.WebSystemsRoot, WebSystemName, addressPostfix);
            }

            ServiceEndpoint serviceEndpoint = new ServiceEndpoint(contractDescription, webHttpBinding, new EndpointAddress(endpointAddress + addressPostfix));
            serviceEndpoint.Behaviors.Add(UserCredentialExtensions.GetWebHttpBehavior());

            return serviceEndpoint;
        }

        #endregion
    }
}
