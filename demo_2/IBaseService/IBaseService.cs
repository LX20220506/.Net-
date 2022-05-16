using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using MyBook.IRepository;
using SqlSugar;

namespace IBaseService
{
    public interface IBaseService<TEntity> where TEntity:class,new()
    {
        Task<bool> CareteAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> EditAsync(TEntity entity);
        Task<TEntity> FindAsync(int id);

        /// <summary>
        /// 通過表達式 查詢數據
        /// </summary>
        /// <param name="fun">例如：Expression<Func<TEntity, bool>> fun=f=>f.name="tom"（就是Lambda表達式）</param>
        /// <returns></returns>
        Task<TEntity> FindAsnync(Expression<Func<TEntity, bool>> fun);
        /// <summary>
        /// 查詢全部數據
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync();
        /// <summary>
        /// 自定義查詢
        /// </summary>
        /// <param name="fun">Lambda表達式</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> fun);
        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="page">頁碼</param>
        /// <param name="size">一頁顯示的數據</param>
        /// <param name="total">總數據數</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total);

        /// <summary>
        /// 自定義分頁
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(int page, int size, int total, Expression<Func<TEntity, bool>> fun);
    }
}
