using jcMPP.PCL.DataLayer.Models;
using Microsoft.Data.Entity;

namespace jcMPP.WebAPI.DataLayer.DbContexts {
    public class FileContext : BaseContext {
        public DbSet<Files> FilesDS { get; set; }
    }
}