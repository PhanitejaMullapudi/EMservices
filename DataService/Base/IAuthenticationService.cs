using DataContract;
using DataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.Base
{
    public interface IAuthenticationService
    {
        ResponseDTO<UserDTO> AuthenticateUser(AuthenticationDTO Mdl);
    }
}
