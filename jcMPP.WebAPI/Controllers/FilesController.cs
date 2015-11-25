using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using jcMPP.PCL.DataLayer.Models;
using jcMPP.WebAPI.DataLayer.DbContexts;

namespace jcMPP.WebAPI.Controllers {
    public class FilesController : ApiController {
        // Client Sends Files to server and then the server returns all files the client doesn't have
        [HttpPost]
        public IEnumerable<Files> Get(List<Guid> clientFiles) {
            using (var fileContext = new FileContext()) {
                return fileContext.FilesDS.Where(a => !clientFiles.Contains(a.ID)).ToList();
            }
        }
    }
}