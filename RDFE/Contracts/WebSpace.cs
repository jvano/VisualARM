using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Vano.Tools.Azure.RDFE.Contracts
{
    [DataContract(Name = "WebSpaceAvailabilityState", Namespace = UriElements.ServiceNamespace)]
    public enum WebSpaceAvailabilityState
    {
        [EnumMember]
        Normal = 0,
        [EnumMember]
        Limited = 1,
    }

    /// <summary>
    /// Webspace.
    /// </summary>
    [DataContract(Namespace = UriElements.ServiceNamespace)]
    public class WebSpace
    {
        /// <summary>
        /// Name.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Name { get; set; }

        /// <summary>
        /// Plan.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Plan { get; set; }

        /// <summary>
        /// Subscription.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Subscription { get; set; }

        /// <summary>
        /// Geographical location.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string GeoLocation { get; set; }

        /// <summary>
        /// Geographical region.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string GeoRegion { get; set; }

        /// <summary>
        /// Resource group.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string ResourceGroup { get; set; }
    }

    /// <summary>
    /// Collection of webspaces.
    /// </summary>
    [CollectionDataContract(Namespace = UriElements.ServiceNamespace)]
    public class WebSpaces : List<WebSpace>
    {

        /// <summary>
        /// Empty collection.
        /// </summary>
        public WebSpaces() { }

        /// <summary>
        /// Initialize collection.
        /// </summary>
        /// <param name="webspaces"></param>
        public WebSpaces(List<WebSpace> webspaces) : base(webspaces) { }

        /// <summary>
        /// Initialize collection.
        /// </summary>
        /// <param name="webspaces"></param>
        public WebSpaces(IEnumerable<WebSpace> webspaces) : base(webspaces) { }
    }
}
