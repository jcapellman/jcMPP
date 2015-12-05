using System.Collections.Generic;

using jcMPP.PCL.DataLayer.Models.Views;
using jcMPP.PCL.Objects;
using jcMPP.WebAPI.DIOL;

using Microsoft.AspNet.Mvc;

namespace jcMPP.WebAPI.Controllers {
    [Route("api/Files")]
    public class FilesController : Controller {
        // Client Sends Files to server and then the server returns all files the client doesn't have
        [HttpPost]
        public CTO<List<GetActiveFilesVIEW>> Get(List<int> clientFiles) {
            return new File().GetFilesForClient(clientFiles);
        }
    }
}