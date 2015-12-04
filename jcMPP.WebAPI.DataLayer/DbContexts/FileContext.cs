using jcMPP.PCL.DataLayer.Models;
using jcMPP.PCL.DataLayer.Models.Views;
using jcMPP.PCL.Enums;

using Microsoft.Data.Entity;

namespace jcMPP.WebAPI.DataLayer.DbContexts {
    public class FileContext : BaseContext {
        public DbSet<Files> FilesDS { get; set; }

        public DbSet<GetActiveFilesVIEW> ActiveFilesDS { get; set; }

        public void AddFile(string content, ASSET_TYPES assetType) {
            var file = new Files {
                AssetTypeID = (int) assetType,
                Content = content
            };
            
            FilesDS.Add(file);
            SaveChanges();
        }
    }
}