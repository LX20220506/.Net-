using Demo.BingFa.Entity;
using System;
using System.Linq.Expressions;

namespace Demo.BingFa.IRepository
{
    public interface IOrderRepository: IRepositoyBase<Order> 
    {
        Order QueryOrder(Expression<Func<Order, bool>> expression);
    }
}
