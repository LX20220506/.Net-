using Demo.Redis.EF;
using Demo.Redis.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.Redis.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RedisTestDbContext _dbContext;

        public RepositoryBase(RedisTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            //EntityEntry e1 = _dbContext.Entry(entity); // 查询状态

            //string ss = e1.DebugView.LongView;// 快照信息
        }

        public void Edit(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression);
        }

        public IQueryable<T> QueryAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
