using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Repository.Base;

namespace DataLayer.UOW
{
    public class UnitOfWork : IDisposable
    {
        #region"variable            "
        private ExpenseManagerDB context;
        private bool disposed = false;

        private GenericRepository<User> userRepository;
        
        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        private GenericRepository<Role> roleRepository;

        public GenericRepository<Role> RoleRepository
        {
            get
            {

                if (this.roleRepository == null)
                {
                    this.roleRepository = new GenericRepository<Role>(context);
                }
                return roleRepository;
            }
        }
        #endregion

        #region"constructor         "
        public UnitOfWork(ExpenseManagerDB DB)
        {
            this.context = DB;
        }
        #endregion

        #region"Save                "
        public void Save()
        {
            context.SaveChanges();
        }
        #endregion

        #region"Dispose             "
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        #endregion

        #region"Dispose             "
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
