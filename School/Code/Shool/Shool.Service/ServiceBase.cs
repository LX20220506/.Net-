using Shool.IRepository;
using Shool.IService;
using Shool.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shool.Service
{
    public class ServiceBase<TEntity>:IServiceBase<TEntity> where TEntity :class, new()
    {
        //这地方比较特殊 使用仓储层的接口 个人理解：将IRepositoryBase当做数据库上下文使用
        //IRepositoryBase对象是从子类中传进来的
        //该类(ServiceBase)的子类的构造函数中会注入 仓储对象；该对象会赋值给父类对象(_repositoryBase)
        //注：_repositoryBase 的访问修饰符是 protected[只有本类和子类可用]
        protected IRepositoryBase<TEntity> _repositoryBase { get; set; }

        public async Task<bool> AddAsync(TEntity entity)
        {
            return await _repositoryBase.AddAsync(entity);
        }

        public async Task<bool> AddList(List<TEntity> entities)
        {
            return await _repositoryBase.AddListAsync(entities);
        }

        public async Task<int> AddReturnIdentityAsync(TEntity entity)
        {
            return await _repositoryBase.AddReturnIdentityAsync(entity);
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            return await _repositoryBase.EditAsync(entity);
        }

        public async Task<bool> EditAsync(List<TEntity> entities)
        {
            return await _repositoryBase.EditAsync(entities);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _repositoryBase.FindAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync()
        {
            return await _repositoryBase.QueryAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _repositoryBase.QueryAsync(func);
        }

        public async Task<bool> RemoveAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _repositoryBase.RemoveAsync(func);
        }

        public async Task<bool> RemoveListAsync(dynamic[] entitys)
        {
            return await _repositoryBase.RemoveListAsync(entitys);
        }

        public async Task<List<TEntity>> SelectPageListAsync(Expression<Func<TEntity, bool>> func, int pageIndex, int pageSize, RefAsync<int> totalCount, Expression<Func<TEntity, object>> orderFunc = null, OrderByType order = OrderByType.Asc)
        {
            return await _repositoryBase.SelectPageListAsync(func,pageIndex,pageSize,totalCount,orderFunc,order);
        }

        public async Task<List<TEntity>> SelectPageListAsync(int pageIndex, int pageSize, RefAsync<int> totalCount, Expression<Func<TEntity, bool>> func)
        {
            return await _repositoryBase.SelectPageListAsync(pageIndex, pageSize, totalCount, func);
        }
    }
}
