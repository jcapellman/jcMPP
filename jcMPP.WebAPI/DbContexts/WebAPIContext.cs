using jcMPP.PCL.DataLayer.Models;
using jcMPP.PCL.Enums;

using Microsoft.Data.Entity;

namespace jcMPP.WebAPI.DbContexts {
    public class WebAPIContext : BaseContext {
        public DbSet<WebAPICalls> WebAPISet { get; set; }
    
        public void RecordResponse(WebAPIResponses response, double duration) {
            var responseCall = new WebAPICalls {
                WebAPICallID = (int)response,
                Duration = duration
            };

            WebAPISet.Add(responseCall);
            SaveChanges();
        }
    }
}