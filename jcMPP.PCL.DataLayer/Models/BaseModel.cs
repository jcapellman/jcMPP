using System;

namespace jcMPP.PCL.DataLayer.Models {
    public class BaseModel {
        public Guid ID { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }
    }
}