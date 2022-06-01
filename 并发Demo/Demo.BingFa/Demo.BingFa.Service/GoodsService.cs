using Demo.BingFa.Base.RabbitMQ.Producer;
using Demo.BingFa.Entity;
using Demo.BingFa.IRepository;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Demo.BingFa.Service
{
    public class GoodsService : IGoodsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GoodsService(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// 判断是否存在库存
        /// </summary>
        /// <param name="goodsid"></param>
        /// <returns></returns>
        public bool CheckStock(int goodsid)
        {
            Goods goods = _repositoryWrapper.Goods.FindAsync(g=>g.Id==goodsid);
            if (goods.Stock>0)
            {
                // 修改商品库存
                goods.Stock--;
                _repositoryWrapper.Goods.Update(goods);
                return true;
            }
            return false;
        }

        public async Task CreateGoodsAsync(string goodsName, double price, int stock)
        {
            _repositoryWrapper.Goods.Create(new Goods() {GoodsNmae=goodsName,Price=price,Stock=stock });
            await _repositoryWrapper.SaveAsync();
        }

        public async Task UpdateStockAsync(int goodsid, int stock)
        {
            _repositoryWrapper.Goods.UpdateStokc(goodsid, stock);
            await _repositoryWrapper.SaveAsync();
        }
    }
}
