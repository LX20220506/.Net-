using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Goods.Repository
{
    public interface IRepositoryBase<TDbContext,T> where TDbContext:DbContext where T: new()
    {
        public Task<int> InsertAsync(TDbContext dbContext,T entity);


        public Task<int> DeleteAsync(TDbContext dbContext,T entity);


        public Task<int> EditAsync(TDbContext dbContext,T entity );


       // public Task<List<T>> QueryAsync(TDbContext dbContext, Expression<Func<T,bool>> expression);
    }
}
