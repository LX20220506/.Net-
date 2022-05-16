using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shool.IRepository
{
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        #region 添加
        public Task<bool> AddAsync(TEntity entity);

        public Task<int> AddReturnIdentityAsync(TEntity entity);

        public Task<bool> AddListAsync(List<TEntity> entities);
        #endregion

        #region 删除
        public Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> func);

        public Task<bool> RemoveListAsync(dynamic[] entitys);
        #endregion

        #region 修改
        public Task<bool> EditAsync(TEntity entity);

        public Task<bool> EditAsync(List<TEntity> entities);
        #endregion

        #region 查询
        public Task<List<TEntity>> QueryAsync();

        public Task<List<TEntity>> QueryAsync(Expression<Func<TEntity,bool>> func);

        public Task<List<TEntity>> SelectPageListAsync(Expression<Func<TEntity, bool>> func, int pageIndex, int pageSize, RefAsync<int> totalCount, Expression<Func<TEntity, object>> orderFunc = null, OrderByType order = OrderByType.Asc);

        public Task<List<TEntity>> SelectPageListAsync(int pageIndex, int pageSize, RefAsync<int> totalCount,Expression<Func<TEntity,bool>> func);


        public Task<TEntity> FindAsync(Expression<Func<TEntity,bool>> func);
        #endregion
    }
}
