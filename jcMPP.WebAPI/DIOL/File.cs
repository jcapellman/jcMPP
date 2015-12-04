using jcMPP.PCL.DataLayer.Models;
using jcMPP.PCL.Enums;
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
    }
}