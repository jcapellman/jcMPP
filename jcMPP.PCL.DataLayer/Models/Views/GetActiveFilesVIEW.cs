using System;

namespace jcMPP.PCL.DataLayer.Models.Views {
    public class GetActiveFilesVIEW {
        public Guid ID { get; set; }

        public Guid AssetTypeID { get; set; }

        public string Content { get; set; }
    }
}