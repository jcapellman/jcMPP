using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace jcMPP.WebAPI.DbContexts {
    public class BaseContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(Startup.Configuration["Data:DefaultConnection:ConnectionString"]);
        }
    }
}