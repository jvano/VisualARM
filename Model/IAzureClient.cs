using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vano.Tools.Azure.Model
{
    interface IAzureClient
    {
        string ResouceManagerEndpoint { get; }

        HttpHeadersProcessor HttpHeadersProcessor { get; set; }

        Task<string> GetAuthSecret(string tenantId = null);

        Task<IEnumerable<Subscription>> GetSubscriptions(CancellationToken cancellationToken = new CancellationToken());

        Task<IEnumerable<Location>> GetLocations(Subscription subscription, CancellationToken cancellationToken = new CancellationToken());

        Task<string> CallAzureResourceManager(string method, string path, string token, string body = null, Dictionary<string, string> parameters = null, string armEndpoint = null, string apiVersion = null, bool displaySecrets = false, CancellationToken cancellationToken = new CancellationToken());
    }
}
