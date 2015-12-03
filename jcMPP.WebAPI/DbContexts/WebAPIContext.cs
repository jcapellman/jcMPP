using jcMPP.PCL.DataLayer.Models;
using Microsoft.Data.Entity;

namespace jcMPP.WebAPI.DbContexts {
    public class WebAPIContext : BaseContext {
        public DbSet<WebAPICalls> WebAPISet { get; set; }
    }
}