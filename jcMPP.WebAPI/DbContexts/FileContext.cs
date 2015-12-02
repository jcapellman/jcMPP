using jcMPP.PCL.DataLayer.Models;
using jcMPP.PCL.DataLayer.Models.Views;

using Microsoft.Data.Entity;

namespace jcMPP.WebAPI.DbContexts {
    public class FileContext : BaseContext {
        public DbSet<Files> FilesDS { get; set; }

        public DbSet<GetActiveFilesVIEW> ActiveFilesDS { get; set; } 
    }
}