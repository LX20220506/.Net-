using Demo.BingFa.Base.RabbitMQ.Consumer;
using Demo.BingFa.Entity.RabbitMqMessageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BingFa.Service
{
    public interface ISeckillService
    {

        void SeckillGoodsMessage(SeckillGoodsMesage mesage);

        Task Seckill(SeckillGoodsMesage message);
    }
}
