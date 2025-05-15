using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Threading.Tasks;

namespace Vano.Tools.Azure.RDFE
{
    public static class UserCredentialExtensions
    {
        public static readonly AADAuthParameter UserAADAuthParameter = new AADAuthParameter()
        {
            AuthorityHost = "https://login.microsoftonline.com/", // Hardcode to Public Azure
            Scope = "https://management.azure.com/.default",
            TenantId = "72f988bf-86f1-41af-91ab-2d7cd011db47", // MSFT
        };

        public static Func<HttpMessageHandler, HttpMessageHandler> GetHttpMessageHandlerFactory(AADAuthParameter parameter) => _ => DefaultAzureCredentialHttpMessageHandler.GetHandler(parameter);

        private class DefaultAzureCredentialHttpMessageHandler : HttpClientHandler
        {
            public static DefaultAzureCredentialHttpMessageHandler GetHandler(AADAuthParameter parameter)
                => new DefaultAzureCredentialHttpMessageHandler(parameter);

            protected readonly AADAuthParameter _parameter;

            public DefaultAzureCredentialHttpMessageHandler(AADAuthParameter parameter)
            {
                _parameter = parameter;
            }

            public virtual string GetToken()
            {
                return DefaultAzureCredentialHelper.GetUserToken(authorityHost: _parameter.AuthorityHost, tenantId: _parameter.TenantId, scope: _parameter.Scope).Token;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
                return base.SendAsync(request, cancellationToken);
            }
        }

        public static WebHttpBehavior GetWebHttpBehavior() => new DefaultAzureCredentialBehavior(UserAADAuthParameter);

        private sealed class DefaultAzureCredentialBehavior : WebHttpBehavior
        {
            private readonly AADAuthParameter _parameter;

            public DefaultAzureCredentialBehavior(AADAuthParameter parameter)
            {
                _parameter = parameter;
            }

            public override void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
            {
                base.ApplyClientBehavior(endpoint, clientRuntime);
                clientRuntime.MessageInspectors.Add(new DefaultAzureCredentialInspector(this));
            }

            private sealed class DefaultAzureCredentialInspector : IClientMessageInspector
            {
                private readonly DefaultAzureCredentialBehavior _behavior;

                public DefaultAzureCredentialInspector(DefaultAzureCredentialBehavior behavior)
                {
                    _behavior = behavior;
                }

                public object BeforeSendRequest(ref Message request, IClientChannel channel)
                {
                    HttpRequestMessageProperty httpRequestMessage;
                    if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out var httpRequestMessageObject))
                    {
                        httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
                    }
                    else
                    {
                        httpRequestMessage = new HttpRequestMessageProperty();
                        request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
                    }

                    // add token
                    httpRequestMessage.Headers["Authorization"] = $"Bearer {DefaultAzureCredentialHttpMessageHandler.GetHandler(_behavior._parameter).GetToken()}";
                    return null;
                }

                public void AfterReceiveReply(ref Message reply, object correlationState)
                {
                }
            }
        }
    }

    public class AADAuthParameter
    {
        public string AuthorityHost { get; set; }
        public string ManagedIdentityId { get; set; }
        public string TenantId { get; set; }
        public string Scope { get; set; }
    }
}