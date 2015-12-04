using jcMPP.PCL.DataLayer.Models;
using jcMPP.PCL.DataLayer.Models.Views;

using Microsoft.Data.Entity;

namespace jcMPP.WebAPI.DataLayer.DbContexts {
    public class FileContext : BaseContext {
        public DbSet<Files> FilesDS { get; set; }

        public DbSet<GetActiveFilesVIEW> ActiveFilesDS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Files>()
                .Property(b => b.Modified)
                .ValueGeneratedOnAddOrUpdate()
                .ForSqlServerHasComputedColumnSql("GETDATE()");

            modelBuilder.Entity<Files>()
                .Property(b => b.Created)
                .ValueGeneratedOnAdd()
                .ForSqlServerHasComputedColumnSql("GETDATE()");

            modelBuilder.Entity<Files>()
                .Property(b => b.Active)
                .ForSqlServerHasDefaultValue("1");
        }
    }
}