using Microsoft.Data.Entity;

namespace jcMPP.WebAPI.DataLayer.DbContexts {
    public class BaseContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=f1coy06qmv.database.windows.net;Database=jcmpp;user id=bosa;Password=Battle4ever;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
        }
    }
}