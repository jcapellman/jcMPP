using System;
using System.Runtime.Serialization;

namespace jcMPP.PCL.Objects.Files {
    [DataContract]
    public class FileResponseItem {
        [DataMember]
        public Guid ID { get; set; }
    }
}