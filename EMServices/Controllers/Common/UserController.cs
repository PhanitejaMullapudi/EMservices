using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMServices.Controllers.Common
{
    [Authorize]
    [RoutePrefix("Common")]
    public class UserController : ApiController
    {
        [Route("Users")]
        public IHttpActionResult GetUserDetails()
        {
            return Ok("");
        }
    }
}
