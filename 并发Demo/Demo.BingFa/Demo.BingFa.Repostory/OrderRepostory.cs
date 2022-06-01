using Demo.BingFa.EF;
using Demo.BingFa.Entity;
using Demo.BingFa.IRepository;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.BingFa.Repostory
{
    public class OrderRepostory:RepositoryBase<Order>,IOrderRepository
    {
        private readonly BingFaDbContext _dbContext;

        public OrderRepostory(BingFaDbContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public Order QueryOrder(Expression<Func<Order,bool>> expression) {
            return _dbContext.Orders.Where(expression).SingleOrDefault();
        }
    }
}
