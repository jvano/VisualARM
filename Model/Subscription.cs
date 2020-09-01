using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Vano.Tools.Azure.Model
{
    public class Subscription
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "subscriptionId")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        public string TenantId { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", DisplayName, Id);
        }
    }
}
