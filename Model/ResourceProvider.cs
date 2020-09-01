using System.Collections.Generic;

namespace Vano.Tools.Azure.Model
{
    public class ResourceGroup
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public IEnumerable<ResourceProvider> Providers { get; set; }
    }

    public class ResourceProvider
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ResourceType> ResourceTypes { get; set; }

        public ResourceGroup Group { get; set; }

    }

    public class ResourceType
    {
        public string Name { get; set; }

        public IEnumerable<string> Locations { get; set; }

        public IEnumerable<string> ApiVersions { get; set; }

        public ResourceProvider Provider { get; set; }
    }
}
