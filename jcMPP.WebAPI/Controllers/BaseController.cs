using System;

using jcMPP.PCL.Enums;
using jcMPP.WebAPI.DbContexts;

using Microsoft.AspNet.Mvc;

namespace jcMPP.WebAPI.Controllers {
    public class BaseController : Controller {
        private readonly DateTime _startTime;

        public BaseController() {
            _startTime = DateTime.Now;
        }

        public T ReturnResponse<T>(T obj, WebAPIResponses response) {
            using (var waContext = new WebAPIContext()) {
                waContext.RecordResponse(response, DateTime.Now.Subtract(_startTime).TotalSeconds);
            }

            return obj;
        }
    }
}