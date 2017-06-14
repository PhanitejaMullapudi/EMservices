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
            UnitOfWork UOW = new UnitOfWork(new ExpenseManagerDB());
        }

        public ResponseDTO<UserDTO> AuthenticateUser(AuthenticationDTO Mdl)
        {
            var Resp = new ResponseDTO<UserDTO>();

            var User = UOW.UserRepository.Get(a => a.UserName == Mdl.UserName && a.Password == Mdl.Password).SingleOrDefault();

            if (User != null)
            {
                Resp.Result = TinyMapper.Map<User, UserDTO>(User);
                Resp.ResposeID = 0;
                Resp.Status = true;
            }
            return Resp;
        }

    }
}
