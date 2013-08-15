using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebChat.Model;

namespace Repositoty
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext dbContext;
        protected DbSet<T> entitySet;

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            this.entitySet.Add(entity);
            this.dbContext.SaveChanges();
            return entity;
        }

        public abstract T Update(int id, T entity);

        public void Delete(int id)
        {
            var entity = this.entitySet.Find(id);

            if (entity != null)
            {
                this.entitySet.Remove(entity);
                this.dbContext.SaveChanges();
            }
        }

        public T Get(int id)
        {
            var entity = this.entitySet.Find(id);
            return entity;
        }

        public IQueryable<T> All()
        {
            return this.entitySet;
        }
    }
}
