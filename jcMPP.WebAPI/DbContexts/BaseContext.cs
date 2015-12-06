using jcMPP.PCL.DataLayer.Models;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace jcMPP.WebAPI.DbContexts {
    public class BaseContext<T> : DbContext where T: BaseModel {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(Startup.Configuration["Data:DefaultConnection:ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<T>()
            .Property(e => e.Created)
            .ValueGeneratedOnAdd()
            .ForSqlServerHasDefaultValueSql("getdate()");

            modelBuilder.Entity<T>()
            .Property(e => e.Modified)
            .ValueGeneratedOnAddOrUpdate()
            .ForSqlServerHasDefaultValueSql("getdate()");

            modelBuilder.Entity<T>()
                .Property(e => e.Active)
                .ValueGeneratedOnAdd()
                .HasDefaultValue(true);
        }
    }
}