using System.Collections.Generic;
using System.Runtime.Serialization;

namespace jcMPP.PCL.Objects.KeepAlive {
    [DataContract]
    public class KeepAliveItem {
        [DataMember]
        public string SiteAddress { get; set; }
        
        [DataMember]
        public string Interval { get; set; }
        
        [DataMember]
        public bool Enabled { get; set; }

        [DataMember]
        public List<KeepAliveHistoryListingItem> History { get; set; }
    }
}