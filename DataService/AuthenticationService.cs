using DataLayer.UOW;
using ServiceContract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataContract;
using DataContract.Common;
using Nelibur.ObjectMapper;

namespace ServiceContract
{
    public class AuthenticationService : IAuthenticationService
    {
        private UnitOfWork UOW { get; set; }

        public AuthenticationService()
        {
            UOW = new UnitOfWork(new ExpenseManagerDB());
        }

        public ResponseDTO<UserDTO> AuthenticateUser(AuthenticationDTO Mdl)
        {
            var Resp = new ResponseDTO<UserDTO>();

            var User = UOW.UserRepository.Get(a => a.UserName == Mdl.UserName && a.Password == Mdl.Password, includeProperties: "UserRoles").SingleOrDefault();

            if (User != null)
            {
                var UserRoles = User.UserRoles.Select(a => a.RoleID).ToList();
                Resp.Result = TinyMapper.Map<User, UserDTO>(User);
                Resp.ResposeID = 0;
                Resp.Status = true;

                if (UserRoles != null)
                {
                    Resp.Result.Roles = UOW.RoleRepository.Get(a => UserRoles.Contains(a.RoleID))?.Select(a => a.RoleCode).ToList();
                }

            }
            return Resp;
        }

    }
}
