using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using jcMPP.PCL.DataLayer.Models.Views;
using jcMPP.PCL.Objects;
using Constants = jcMPP.PCL.Common.Constants;

namespace jcMPP.PCL.Handlers {
    public class FileHandler : BaseHandler {
        public FileHandler() : base(Constants.WEBAPI_ADDRESS, "Files") { }

        public async Task<CTO<List<GetActiveFilesVIEW>>> GetFiles(List<Guid> clientFiles) {
            return await POST<List<Guid>, CTO<List<GetActiveFilesVIEW>>>(string.Empty, clientFiles);
        }
    }
}