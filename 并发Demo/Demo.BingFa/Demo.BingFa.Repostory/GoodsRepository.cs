using Demo.BingFa.EF;
using Demo.BingFa.Entity;
using Demo.BingFa.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BingFa.Repostory
{
    public class GoodsRepository:RepositoryBase<Goods>,IGoodsRepository
    {
        private readonly BingFaDbContext _dbContext;

        public GoodsRepository(BingFaDbContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public void UpdateStokc(int goodsid, int stock)
        {
            Goods goods = _dbContext.Goods.SingleOrDefault(g=>g.Id==goodsid);
            goods.Stock = stock;
        }
    }
}
