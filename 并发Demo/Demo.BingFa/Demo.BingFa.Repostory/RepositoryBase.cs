using Demo.BingFa.EF;
using Demo.BingFa.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.BingFa.Repostory
{
    public class RepositoryBase<T> : IRepositoyBase<T> where T : class
    {
        private readonly BingFaDbContext _dbContext;

        public RepositoryBase(BingFaDbContext dbContext) {
            _dbContext = dbContext;
        } 

        public  void Create(T entity)
        {
            _dbContext.Set<T>().AddAsync(entity);
        }

        public T FindAsync(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).FirstOrDefault();
        }

        public  List<T> QueryAsync()
        {
            return  _dbContext.Set<T>().ToList();
        }

        public List<T> QueryAsync(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).ToList();
        }

        public void Remove(T entity)
        {
             _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
