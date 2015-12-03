using System;

using jcMPP.PCL.DataLayer.Models;
using jcMPP.PCL.Enums;
using jcMPP.WebAPI.DbContexts;

using Microsoft.AspNet.Mvc;

namespace jcMPP.WebAPI.Controllers {
    public class BaseController : Controller {
        public T ReturnResponse<T>(T obj, WebAPIResponses response) {
            using (var waContext = new WebAPIContext()) {
                var responseCall = new WebAPICalls();

                responseCall.WebAPICallID = (int)response;
                responseCall.Active = true;
                responseCall.Modified = DateTime.Now;
                responseCall.Created = DateTime.Now;

                waContext.WebAPISet.Add(responseCall);
                waContext.SaveChanges();
            }

            return obj;
        }
    }
}