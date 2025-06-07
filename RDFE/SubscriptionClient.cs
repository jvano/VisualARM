using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using Vano.Tools.Azure.RDFE.Contracts;

namespace Vano.Tools.Azure.RDFE
{
    public class SubscriptionClient : AdministrationClientBase<ISubscriptionService>, ISubscriptionService, IDisposable
    {
        public SubscriptionClient()
        {
        }

        public SubscriptionClient(ServiceEndpoint endpoint)
            : base(endpoint)
        {
        }

        public Subscriptions GetSubscriptions(string marker, int recordCount, string ownerUserName)
        {
            return this.Channel.GetSubscriptions(marker, recordCount, ownerUserName);
        }

        public Subscription GetSubscription(string name)
        {
            return this.Channel.GetSubscription(name);
        }

        public IEnumerable<string> GetActiveSubscriptions(string activeFromDate, string activeToDate)
        {
            return this.Channel.GetActiveSubscriptions(activeFromDate, activeToDate);
        }

        public WebSpaces GetWebSpaces(string subscriptionName)
        {
            return this.Channel.GetWebSpaces(subscriptionName);
        }

        public WebSpace GetWebSpace(string subscriptionName, string name)
        {
            return this.Channel.GetWebSpace(subscriptionName, name);
        }
    }
}
