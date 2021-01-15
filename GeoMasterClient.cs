using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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

        private SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private string _certThumbprint;
        private X509Certificate2 _cert;

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
            string certThumbprint,
            string apiVersion = "2015-01-01")
        {
            _certThumbprint = certThumbprint;
            _cert = GetCertificate(certThumbprint);
            this.GeoMasterEndpoint = geoMasterEndpoint;
            this.ApiVersion = apiVersion;
        }

        #endregion

        #region Public properties

        public string ResouceManagerEndpoint { get { return this.GeoMasterEndpoint; } }

        public string GeoMasterEndpoint { get; private set; }

        public string ApiVersion { get; private set; }

        #endregion

        #region Public Methods - ARM Operations

        public async Task<IEnumerable<Subscription>> GetSubscriptions()
        {
            List<Subscription> subscriptions = new List<Subscription>();

            try
            {
                SubscriptionClient rdfeClient = GetRdfeSubscriptionClient();            
                var rdfeSubscriptions = rdfeClient.GetSubscriptions(marker: "", recordCount: 100, ownerUserName: "");
                foreach(var rdfeSubscription in rdfeSubscriptions)
                {
                    subscriptions.Add(new Subscription()
                    {
                        Id = rdfeSubscription.Name,
                        DisplayName = string.IsNullOrWhiteSpace(rdfeSubscription.Description) ? "[CSM-Direct]" : rdfeSubscription.Description,
                        TenantId = _certThumbprint
                    });
                }
            }
            catch
            {
                subscriptions.AddRange(new Subscription[]
                {
                    new Subscription()
                    {
                        Id = "00000000-0000-0000-0000-000000000000",
                        DisplayName = "[CSM-Direct]",
                        TenantId = _certThumbprint
                    }
                });
            }

            // Make async call happy
            await Task.Delay(0);

            return subscriptions.OrderBy(sub => sub.DisplayName);
        }

        public async Task<IEnumerable<Location>> GetLocations(Subscription subscription)
        {
            List<Location> locations = new List<Location>();
            try
            {
                SubscriptionClient rdfeClient = GetRdfeSubscriptionClient();
                var webspaces = rdfeClient.GetWebSpaces(subscription.Id);

                foreach (var webspace in webspaces)
                {
                    locations.Add(new Location()
                    {
                        Id = webspace.Name,
                        Name = webspace.GeoRegion,
                        DisplayName = webspace.GeoRegion
                    });
                }
            }
            catch
            {
                // Return a fixed list
                locations.AddRange(new Location[] 
                {
                    new Location()
                    {
                        Id = "USAAnywhere",
                        Name = "USA Anywhere",
                        DisplayName = "USA Anywhere"
                    }
                });
            }

            // Make async call happy
            await Task.Delay(0);

            return locations.OrderBy(sub => sub.DisplayName);
        }

        #endregion

        #region Private Methods - Tokens

        public async Task<string> GetAuthSecret(string tenantId = null)
        {
            return await Task.FromResult<string>(_certThumbprint);
        }

        #endregion

        #region ARM Helper Methods

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

        private async Task<JObject> CallAzureResourceManagerAsJObject(string method, string path, string token = null, string body = null, Dictionary<string, string> parameters = null, string armEndpoint = null, string apiVersion = null)
        {
            string response = await CallAzureResourceManager(method, path, token, body, parameters, armEndpoint, apiVersion);
            if (!string.IsNullOrWhiteSpace(response))
            {
                return JObject.Parse(response);
            }

            return new JObject();
        }

        public async Task<string> CallAzureResourceManager(string method, string path, string token, string body = null, Dictionary<string, string> parameters = null, string armEndpoint = null, string apiVersion = null)
        {
            Uri requestUri = CreateAzureResourceManagerUri(path, parameters, armEndpoint, apiVersion);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Method = method;
            request.ContentType = "application/json";
            request.ClientCertificates.Add(_cert);

            if (string.IsNullOrWhiteSpace(body))
            {
                request.ContentLength = 0;
            }
            else
            {
                using (Stream requestStream = await request.GetRequestStreamAsync())
                {
                    byte[] content = Encoding.UTF8.GetBytes(body);
                    requestStream.Write(content, 0, content.Length);
                }
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                using (Stream receiveStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        return await readStream.ReadToEndAsync();
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (Stream receiveStream = e.Response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            throw new Exception(e.Message + Environment.NewLine + readStream.ReadToEnd());
                        }
                    }
                }

                throw;
            }
        }

        #endregion

        #region Private Static Methods - Certs

        private static X509Certificate2 GetCertificate(string thumbprint)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            try
            {
                var results = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                if (results.Count == 0)
                {
                    throw new Exception(string.Format("Failed to load certificate with thumbprint {0}.", thumbprint));
                }

                return results[0];
            }
            finally
            {
                store.Close();
            }
        }

        #endregion

        private SubscriptionClient GetRdfeSubscriptionClient()
        {
            ServiceEndpoint endpoint = GetRdfeServiceEndpoint(contractDescription: ContractDescription.GetContract(typeof(Vano.Tools.Azure.RDFE.Contracts.ISubscriptionService)), addressPostfix: UriElements.Subscriptions);

            SubscriptionClient client = new SubscriptionClient(endpoint);

            ApplyRdfeClientSettings(client);

            return client;
        }

        private ServiceEndpoint GetRdfeServiceEndpoint(ContractDescription contractDescription, string addressPostfix, bool supportsWebSystem = false)
        {
            // CSM          joaquinvvmssgeo.cloudapp.net:444
            // RDFE         joaquinvvmssgeo.cloudapp.net:443 GEO
            // RDFE         joaquinvvmssgeo.cloudapp.net:454 STAMP

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
            webHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
            webHttpBinding.MaxReceivedMessageSize = MaxReceivedMessageSize;
            webHttpBinding.ReaderQuotas.MaxStringContentLength = MaxStringContentLength;

            // Do not check a validity of the certificate
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            if (supportsWebSystem && !String.IsNullOrEmpty(WebSystemName))
            {
                addressPostfix = String.Format("{0}/{1}/{2}", UriElements.WebSystemsRoot, WebSystemName, addressPostfix);
            }

            ServiceEndpoint serviceEndpoint = new ServiceEndpoint(contractDescription, webHttpBinding, new EndpointAddress(endpointAddress + addressPostfix));
            serviceEndpoint.Behaviors.Add(new WebHttpBehavior());

            return serviceEndpoint;
        }

        private void ApplyRdfeClientSettings<T>(AdministrationClientBase<T> client) 
            where T : class
        {
            client.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My, X509FindType.FindByThumbprint, _certThumbprint);
        }
    }
}
