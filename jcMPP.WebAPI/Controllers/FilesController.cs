using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using jcMPP.PCL.DataLayer.Models.Views;
using jcMPP.PCL.Objects;
using jcMPP.WebAPI.DataLayer.DbContexts;

namespace jcMPP.WebAPI.Controllers {
    public class FilesController : ApiController {
        // Client Sends Files to server and then the server returns all files the client doesn't have
        [HttpPost]
        public CTO<List<GetActiveFilesVIEW>> Get(List<Guid> clientFiles) {
            try {
                using (var fileContext = new FileContext()) {
                    return new CTO<List<GetActiveFilesVIEW>>(fileContext.ActiveFilesDS.Where(a => !clientFiles.Contains(a.ID)).ToList());
                }
            } catch (Exception ex) {
                return new CTO<List<GetActiveFilesVIEW>>(null, ex.ToString());
            }
        }
    }
}