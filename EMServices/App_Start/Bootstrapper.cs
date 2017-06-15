using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMServices
{
    public class Bootstrapper
    {
        public static void Run(HttpConfiguration config)
        {
            AutofacWebapiConfig.Initialize(config);
        }
    }
}