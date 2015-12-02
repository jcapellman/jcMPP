using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace jcMPP.WebAPI.DbContexts {
    public class BaseContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = Startup.Configuration.Get("Data:DefaultConnection:ConnectionString");
            optionsBuilder.UseSqlServer(connection);
        }
    }
}