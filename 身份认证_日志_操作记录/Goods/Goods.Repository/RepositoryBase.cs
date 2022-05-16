using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Goods.Repository
{
    public class RepositoryBase<TDbContext, T> : IRepositoryBase<TDbContext, T> where TDbContext : DbContext where T : new()
    {
        public async Task<int> DeleteAsync(TDbContext dbContext, T entity)
        {
            dbContext.Remove(entity);
            return await dbContext.SaveChangesAsync();
        }


        public async Task<int> EditAsync(TDbContext dbContext, T entity)
        {
            dbContext.Update(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(TDbContext dbContext, T entity)
        {
            await dbContext.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        //public Task<List<T>> QueryAsync(TDbContext dbContext, Expression<Func<T, bool>> expression)
        //{
        //    dbContex.Set<T>();
        //}
    }
}
