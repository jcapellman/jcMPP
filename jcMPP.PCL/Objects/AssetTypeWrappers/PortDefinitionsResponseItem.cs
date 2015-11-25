using System.Collections.Generic;
using System.Runtime.Serialization;

using jcMPP.PCL.DataLayer.Models.Views;
using jcMPP.PCL.Objects.Ports;

namespace jcMPP.PCL.Objects.AssetTypeWrappers {
    [DataContract]
    public class PortDefinitionsResponseItem {
        [DataMember]
        public IEnumerable<PortListingItem> Ports { get; set; } 
    }
}