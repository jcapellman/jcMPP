using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using jcMPP.PCL.DataLayer.Models;
using jcMPP.WebAPI.DataLayer.DbContexts;

namespace jcMPP.WebAPI.Controllers {
    public class FilesController : ApiController {
        public IEnumerable<Files> Get(List<Guid> clientFiles) {
            using (var fileContext = new FileContext()) {
                return fileContext.FilesDS.ToList();
            }
        }
    }
}