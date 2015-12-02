using System;
using System.Collections.Generic;
using System.Linq;

using jcMPP.PCL.DataLayer.Models.Views;
using jcMPP.PCL.Objects;
using jcMPP.WebAPI.DbContexts;

using Microsoft.AspNet.Mvc;

namespace jcMPP.WebAPI.Controllers {
    [Route("api/[controller]")]
    public class FilesController : Controller {
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