using System;
using System.Collections.Generic;

using jcMPP.PCL.DataLayer.Models;
using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects.AssetTypeWrappers;
using jcMPP.PCL.Objects.Ports;
using jcMPP.PCL.PlatformAbstractions;
using jcMPP.WebAPI.DataLayer.DbContexts;

namespace jcMPP.Admin.Console {
    public class Updater {
        public void Run() {
            var portList = new List<PortListingItem>
            {
                    new PortListingItem("FTP", 21),
                    new PortListingItem("SSH", 22),
                    new PortListingItem("SMTP", 25),
                    new PortListingItem("HTTP", 80)
                };

            using (var fileCT = new FileContext()) {
                var file = new Files();
                file.AssetTypeID = (int)ASSET_TYPES.PORT_DEFINITIONS;
                file.Content = BasePA.GetJSONStringFromT(new PortDefinitionsResponseItem { Ports = portList });
                file.Active = true;
                file.Modified = DateTime.Now;
                file.Created = DateTime.Now;
                file.ID = Guid.NewGuid();

                fileCT.FilesDS.Add(file);
                fileCT.SaveChanges();
            }
        }
    }
}