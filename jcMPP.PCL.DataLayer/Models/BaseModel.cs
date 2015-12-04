using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace jcMPP.PCL.DataLayer.Models {
    public class BaseModel {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        public DateTime Modified { get; set; }
        
        public DateTime Created { get; set; }

        public bool Active { get; set; }
    }
}