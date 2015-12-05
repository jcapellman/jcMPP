using System;
using System.Collections.Generic;
using System.Linq;

using jcMPP.PCL.DataLayer.Models;
using jcMPP.PCL.DataLayer.Models.Views;
using jcMPP.PCL.Enums;
using jcMPP.PCL.Objects;
using jcMPP.WebAPI.DbContexts;

namespace jcMPP.WebAPI.DIOL {
    public class File {
        public void AddFile(string content, ASSET_TYPES assetType) {
            using (var fileContext = new FileContext()) {
                var file = new Files {
                    AssetTypeID = (int)assetType,
                    Content = content
                };

                fileContext.FilesDS.Add(file);
                fileContext.SaveChanges();
            }
        }

        public CTO<List<GetActiveFilesVIEW>> GetFilesForClient(List<int> clientFiles) {
            try {
                using (var fileContext = new FileContext()) {
                    return
                        new CTO<List<GetActiveFilesVIEW>>(
                            fileContext.ActiveFilesDS.Where(a => !clientFiles.Contains(a.ID)).ToList());
                }
            } catch (Exception ex) {
                return new CTO<List<GetActiveFilesVIEW>>(null, ex);
            }
        }
    }
}