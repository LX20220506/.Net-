using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Models;
using MyBook.IRepository;
using SqlSugar;
using SqlSugar.IOC;

namespace MyBook.Repository
{
    public class BaseRepository<TEntity>:SimpleClient<TEntity>,IBaseRepository<TEntity> where TEntity:class,new()
    {
        public BaseRepository(ISqlSugarClient context = null) : base(context)
        {
           base.Context = DbScoped.Sugar;
            // 创建数据库
            //base.Context.DbMaintenance.CreateDatabase();
            //// 创建表
            //base.Context.CodeFirst.InitTables(
            //  typeof(BookInfo),
            //  typeof(TypesInfo),
            //  typeof(Admin)
            //  );
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> CareteAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        /// <summary>
        /// 通過id，刪除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            return await base.DeleteByIdAsync(id);
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
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
