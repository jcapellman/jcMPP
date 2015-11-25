using Microsoft.Data.Entity;

namespace jcMPP.WebAPI.DataLayer.DbContexts {
    public class BaseContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=jcmpp;user id=jcmppsa;Password=jcmppsa;persist security info=True;Connection Timeout=30;");
        }
    }
}