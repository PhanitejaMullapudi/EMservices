using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DataLayer.Repository.Base
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal  ExpensesManagerDB context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ExpensesManagerDB context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        #region"Get                 "
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        #endregion

        #region"GetByID             "
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        #endregion

        #region"Insert              "
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        #endregion

        #region"Delete              "
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        #endregion

        #region"Delete              "
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        #endregion

        #region"Update              "
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        #endregion


    }
}
