using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace jcMPP.PCL.Objects.KeepAlive {
    [DataContract]
    public class KeepAliveItem {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string SiteAddress { get; set; }
        
        [DataMember]
        public string Interval { get; set; }
        
        [DataMember]
        public bool Enabled { get; set; }

        [DataMember]
        public bool AlertOnFailure { get; set; }

        [DataMember]
        public List<KeepAliveHistoryListingItem> History { get; set; }

        public KeepAliveItem() { }

        public KeepAliveItem(KeepAliveListingItem listingItem) {
            SiteAddress = listingItem.Description;
            Enabled = listingItem.IsEnabled;
            ID = listingItem.ID;
        }
    }
}