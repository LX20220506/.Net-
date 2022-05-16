using System;
using Models;
using IBaseService;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using SqlSugar;
using MyBook.IRepository;

namespace MyBook.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        //从子类的构造函数中传入，_iBaseRepository可以理解為數據庫上下文對象
        //通過子類的創建，可以將對象注入到子類的構造函數中
        //在子類的構造函數中，對父類中的數據庫上下文對象進行賦值
        protected IBaseRepository<TEntity> _iBaseRepository;

        //添加對象
        public Task<bool> CareteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsnync(Expression<Func<TEntity, bool>> fun)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> fun)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAsync(int page, int size, int total, Expression<Func<TEntity, bool>> fun)
        {
            throw new NotImplementedException();
        }
    }
}
