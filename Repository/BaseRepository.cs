using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Repository
{
    abstract public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext dbContext;
        protected DbSet<T> entitySet;

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<T>();
        }

        public virtual T Add(T newEntity)
        {
            this.entitySet.Add(newEntity);
            this.dbContext.SaveChanges();
            return newEntity;
        }

        public virtual T Update(int id, T entity)
        {
            return entity;
        }
        
        public void Delete(int id)
        {
            var entityToRemove = this.entitySet.Find(id);

            if(entityToRemove != null)
            {
                this.entitySet.Remove(entityToRemove);
                this.dbContext.SaveChanges();
            }

        }

        public T Get(int id)
        {
            var searchEntity = this.entitySet.Find(id);
            return searchEntity;
        }

        public IQueryable<T> All()
        {
            return this.entitySet;
        }
    }
}
