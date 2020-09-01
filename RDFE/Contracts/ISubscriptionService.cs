using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Vano.Tools.Azure.RDFE.Contracts
{
    [ServiceContract]
    public interface ISubscriptionService
    {
        #region Subscription operations

        [Description("Returns all subscriptions")]
        [WebGet(UriTemplate = UriElements.Root + UriElements.ContinuationParameters + "&" + UriElements.OwnerUserNameParameter)]
        Subscriptions GetSubscriptions(string marker, int recordCount, string ownerUserName);

        [Description("Returns the subscription details")]
        [WebGet(UriTemplate = UriElements.NameTemplateParameter)]
        Subscription GetSubscription(string name);

        [Description("Get subscriptions that had activity of scaling in given time period.")]
        [WebGet(UriTemplate = UriElements.ActiveSubscriptions)]
        IEnumerable<string> GetActiveSubscriptions(string activeFromDate, string activeToDate);

        #endregion Subscription operations

        #region WebSpace operations

        [Description("Gets all webspaces for subscription")]
        [WebGet(UriTemplate = UriElements.WebSpacesRoot)]
        WebSpaces GetWebSpaces(string subscriptionName);

        [Description("Gets a webspace")]
        [WebGet(UriTemplate = UriElements.WebSpacesRoot + UriElements.NameTemplateParameter)]
        WebSpace GetWebSpace(string subscriptionName, string name);

        #endregion WebSpace operations
    }
}
