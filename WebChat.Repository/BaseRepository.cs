using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Models;
using System.Data.Entity;

namespace WebChat.Repository
{
    abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext dbContext;
        protected DbSet<T> entitySet;

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<T>();
        }

        public virtual T Add(T entity)
        {
            this.entitySet.Add(entity);
            this.dbContext.SaveChanges();
            return entity;
        }

        public abstract T Update(int id, T entity);

        public virtual void Delete(int id)
        {
            var entity = this.entitySet.Find(id);
            if (entity != null)
            {
                this.entitySet.Remove(entity);
                this.dbContext.SaveChanges();
            }
        }

        public virtual T Get(int id)
        {
            T entity = this.entitySet.Find(id);
            return entity;
        }

        public IQueryable<T> All()
        {
            return this.entitySet;
        }

        public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, int, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
