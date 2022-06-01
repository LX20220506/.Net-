using Demo.BingFa.Entity;
using Demo.BingFa.Entity.RabbitMqMessageModel;
using System.Threading.Tasks;

namespace Demo.BingFa.Service
{
    public interface IOrderService
    {
        Task CreateOrder(int userId, int goodsId);

        Task CreateSeckillOrder(int userId, int goodsId);

        Task UpdateOrderState(SeckillGoodsMesage seckillGoodsMesage, int state);

        void Update(Order order);
    }
}
