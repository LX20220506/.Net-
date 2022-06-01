using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo.BingFa.IRepository
{
    public interface IRepositoyBase<T> where T : class
    {
        List<T> QueryAsync();
        List<T> QueryAsync(Expression<Func<T, bool>> expression);
        T FindAsync(Expression<Func<T,bool>> expression);


        void Create(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
