using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Autofac.Integration.Owin;
using Autofac;
using ServiceContract.Base;
using System.Security.Claims;
using DataContract;
using ServiceContract;

namespace EMServices
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var autofacLifetimeScope = OwinContextExtensions.GetAutofacLifetimeScope(context.OwinContext);
            //var AuthenticationService = autofacLifetimeScope.Resolve<IAuthenticationService>();
            
            //var AuthenticationService = test.Resolve<IAuthenticationService>();

            IAuthenticationService AService = new AuthenticationService();

            if (AService != null)
            {
                var Resp = AService.AuthenticateUser(new AuthenticationDTO() { UserName = context.UserName, Password = context.Password });

                if (Resp == null || Resp.Result == null || Resp.Status == false || Resp.ResposeID != 0)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));

                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);

            }
        }
    }
}