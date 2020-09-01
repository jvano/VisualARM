using System.ServiceModel.Description;

namespace Vano.Tools.Azure.RDFE
{
    public class AdministrationEndpointBehavior<T> : IEndpointBehavior where T : class
    {
        private AdministrationClientBase<T> _client;

        public AdministrationEndpointBehavior(AdministrationClientBase<T> client)
        {
            _client = client;
        }


        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            AdministrationMessageInspector<T> inspector = new AdministrationMessageInspector<T>(_client);
            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
