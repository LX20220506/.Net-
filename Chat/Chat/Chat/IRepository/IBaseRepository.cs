using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chat.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity:class,new()
    {
        Task<IEnumerable<TEntity>> QuseryAsync();

        Task<IEnumerable<TEntity>> QuseryAsync(Expression<Func<TEntity,bool>> func);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> func);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<bool> InsertListAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> DelectAsync(IEnumerable<TEntity> entities);

        Task<bool> EditAsync(TEntity entity);
        
    }
}
