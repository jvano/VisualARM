using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Vano.Tools.Azure.RDFE
{
    public class AdministrationClientBase<T> : ClientBase<T>
        where T : class
    {
        public Guid RequestId = Guid.Empty;
        public bool Tracing = false;
        public string AuthCertThumbprint = null;
        public string Marker = null;
        public string AcceptLanguage = null;
        public string Warning = null;
        public string StampName = null;
        public string ApiVersion = "2014-03-01";
        public string UserAgent = null;
        public Dictionary<string, string> ExtraHeaders = new Dictionary<string, string>();

        public bool TraceRequestReply = false;

        public AdministrationClientBase()
        {
            Endpoint.Behaviors.Add(new AdministrationEndpointBehavior<T>(this));

            this.RequestId = Guid.NewGuid();
        }

        public AdministrationClientBase(ServiceEndpoint endpoint)
            : base(endpoint.Binding, endpoint.Address)
        {
            foreach (var behavior in endpoint.Behaviors)
            {
                base.Endpoint.Behaviors.Add(behavior);
            }

            Endpoint.Behaviors.Add(new AdministrationEndpointBehavior<T>(this));

            this.RequestId = Guid.NewGuid();
        }

        public void AddTraceInformation(string t)
        {
            Console.WriteLine(t);
        }

        public string LastRequestId
        {
            get
            {
                return this.RequestId.ToString();
            }
        }
    }
}
