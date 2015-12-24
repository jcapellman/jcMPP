using System;
using System.Runtime.Serialization;

namespace jcMPP.PCL.Objects.KeepAlive {
    [DataContract]
    public class KeepAliveListingItem {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime LastReport { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }
    }
}