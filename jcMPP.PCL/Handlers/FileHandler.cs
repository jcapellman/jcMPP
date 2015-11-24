using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using jcMPP.PCL.DataLayer.Models;

using Constants = jcMPP.PCL.Common.Constants;

namespace jcMPP.PCL.Handlers {
    public class FileHandler : BaseHandler {
        public FileHandler() : base(Constants.WEBAPI_ADDRESS, "Files") { }

        public async Task<List<Files>> GetFiles(List<Guid> clientFiles) {
            return await POST<List<Guid>, List<Files>>(string.Empty, clientFiles);
        }
    }
}