using System;

namespace jcMPP.PCL.DataLayer.Models {
    public class Files : BaseModel {
        public string Content { get; set; }

        public Guid AssetTypeID { get; set; }
    }
}