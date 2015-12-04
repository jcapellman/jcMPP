using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace jcMPP.PCL.DataLayer.Models {
    public class BaseModel {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Modified { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }

        public bool Active { get; set; }
    }
}