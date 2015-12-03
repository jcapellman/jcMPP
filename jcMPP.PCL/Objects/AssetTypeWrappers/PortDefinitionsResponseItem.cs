using System.Collections.Generic;

using jcMPP.PCL.Objects.Ports;
using Newtonsoft.Json;

namespace jcMPP.PCL.Objects.AssetTypeWrappers {
    public class PortDefinitionsResponseItem {
        [JsonProperty("ports")]
        public List<PortListingItem> Ports { get; set; } 
    }
}