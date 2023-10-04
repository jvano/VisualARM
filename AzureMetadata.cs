using System;

namespace Vano.Tools.Azure
{
    public class AzureMetadata
    {
        public string LoginEndpoint { get; set; }

        public string[] Audiences { get; set; }

        public bool IsADFS
        {
            get
            {
                return IsADFSAuthentication(this.LoginEndpoint);
            }
        }

        internal static bool IsADFSAuthentication(string loginEndpoint)
        {
            if (string.IsNullOrWhiteSpace(loginEndpoint))
            {
                throw new ArgumentNullException("LoginEndpoint");
            }

            Uri loginEndpointUri = new Uri(loginEndpoint);

            return (loginEndpointUri.LocalPath != "/");
        }
    }
}