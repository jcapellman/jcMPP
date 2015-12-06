using System.Collections.Generic;
using System.Runtime.Serialization;

using jcMPP.PCL.Enums;

namespace jcMPP.PCL.Objects.Hashes {
    [DataContract]
    public class HashCrackRequestItem {
        [DataMember]
        public List<string> Hashes { get; set; }

        [DataMember]
        public int? MaximumLength { get; set; }

        [DataMember]
        public int? MinimumLength { get; set; }

        [DataMember]
        public HashTypes? HashType { get; set; }
    }
}