using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using jcMPP.PCL.Enums;

namespace jcMPP.PCL.Objects.Hashes {
    [DataContract]
    public class HashCrackResponseItem {
        [DataMember]
        public List<string> Unhashed { get; set; }

        [DataMember]
        public List<string> Hashed { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public HashTypes? HashType { get; set; }
    }
}