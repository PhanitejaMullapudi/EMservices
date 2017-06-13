﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
namespace DataLayer.UOW
{
    public class UnitOfWork : IDisposable
    {
        #region"variable            "
        private ExpenseManagerDB context;
        private bool disposed = false;
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