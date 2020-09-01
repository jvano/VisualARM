using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Vano.Tools.Azure.RDFE.Contracts
{
    /// <summary>
    /// An Azure subscription.
    /// </summary>
    [DataContract(Namespace = UriElements.ServiceNamespace)]
    public class Subscription
    {
        /// <summary>
        /// Name of the subscription.
        /// </summary>
        [DataMember]
        [Description("Subscription Name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the subscription.
        /// </summary>
        [DataMember]
        [Description("Subscription Description")]
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether the subscription is suspended.
        /// </summary>
        [DataMember]
        public bool? Suspended { get; set; }

        /// <summary>
        /// Name of the user who is the owner of the Subscription.
        /// </summary>
        [DataMember]
        public string OwnerUserName { get; set; }

        /// <summary>
        /// <code>true</code> if this subscription is hosted on reserved storage; otherwise, <code>false</code>.
        /// </summary>
        
        [DataMember]
        public bool? EnabledForReservedStorage { get; set; }

        /// <summary>
        /// <code>true</code> if this subscription is enabled for preview features (ministamps); otherwise, <code>false</code>.
        /// </summary>
        
        [DataMember]
        public bool? PreviewEnabled { get; set; }

        /// <summary>
        /// OrgDomains this user is part of.
        /// </summary>
        
        [DataMember]
        public List<string> OrgDomains { get; set; }

        /// <summary>
        /// Specifies whether or not this subscription is a higher priviledge first party (e.g. internal Microsoft customer)
        /// </summary>
        
        [DataMember(IsRequired = false)]
        public bool? IsFirstParty { get; set; }

        /// <summary>
        /// Ignore quotas expiration time for subscription. Quotas are ignored only if 
        /// current time is less than expiration time. This is a per stamp setting on subscription
        /// </summary>
        
        [DataMember(EmitDefaultValue = false)]
        public DateTime? IgnoreQuotaExpirationTimeUtc { get; set; }

        /// <summary>
        /// Offer types from subscription.
        /// </summary>
        
        [DataMember]
        public string OfferTypes { get; set; }

        /// <summary>
        /// Quota type
        /// Null for legacy support
        /// Unlimied for Resource Provider admin
        /// Explicit for ones using SubscriptionPolicy references
        /// </summary>
        
        [DataMember(IsRequired = false)]
        public string QuotaType { get; set; }
    }

    /// <summary>
    /// Collection of subscriptions.
    /// </summary>
    [CollectionDataContract(Namespace = UriElements.ServiceNamespace)]
    public class Subscriptions : List<Subscription>
    {

        /// <summary>
        /// Empty collection.
        /// </summary>
        public Subscriptions() { }

        /// <summary>
        /// Initialize collection.
        /// </summary>
        /// <param name="subscriptions"></param>
        public Subscriptions(List<Subscription> subscriptions) : base(subscriptions) { }
    }
}
