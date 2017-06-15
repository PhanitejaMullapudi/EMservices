using ServiceContract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMServices.Controllers
{
    [RoutePrefix("Test")]
    
    public class TestController : ApiController
    {
        private ITestService _authenticationService;

        public TestController(ITestService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public IHttpActionResult Get() {
           return Ok("test");
        }

        
    }
}
