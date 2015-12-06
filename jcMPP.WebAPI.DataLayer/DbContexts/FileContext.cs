using jcMPP.PCL.DataLayer.Models;
using jcMPP.PCL.DataLayer.Models.Views;
using Microsoft.Data.Entity;

namespace jcMPP.WebAPI.DataLayer.DbContexts {
    public class FileContext : BaseContext<Files> {
        public DbSet<Files> FilesDS { get; set; }

        public DbSet<GetActiveFilesVIEW> ActiveFilesDS { get; set; } 
    }
}