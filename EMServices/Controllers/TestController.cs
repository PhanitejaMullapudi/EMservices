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
        private IAuthenticationService _authenticationService;

        public TestController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult Get() {
           return Ok("test");
        }

        
    }
}
