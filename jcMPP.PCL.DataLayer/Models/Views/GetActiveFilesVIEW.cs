using System;
using System.Runtime.Serialization;

namespace jcMPP.PCL.DataLayer.Models.Views {
    [DataContract]
    public class GetActiveFilesVIEW {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public int AssetTypeID { get; set; }

        [DataMember]
        public string Content { get; set; }
    }
}