using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoty
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(int id, T entity);
        void Delete(int id);
        T Get(int id);
        IQueryable<T> All();
    }
}
