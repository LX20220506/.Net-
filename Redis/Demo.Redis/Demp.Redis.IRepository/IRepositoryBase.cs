using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Demo.Redis.IRepository
{
    public interface IRepositoryBase<T> where T :class
    {
        IQueryable<T> QueryAll();
        IQueryable<T> Find(Expression<Func<T,bool>> expression);

        void Create(T entity);
        void Edit(T entity);

        void Remove(T entity);
    }
}
