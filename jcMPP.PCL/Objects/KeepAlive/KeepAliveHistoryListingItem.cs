using System;
using System.Runtime.Serialization;

namespace jcMPP.PCL.Objects.KeepAlive {
    [DataContract]
    public class KeepAliveHistoryListingItem {
        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public bool Success { get; set; }
    }
}