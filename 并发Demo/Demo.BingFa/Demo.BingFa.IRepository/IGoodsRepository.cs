using Demo.BingFa.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BingFa.IRepository
{
    public interface IGoodsRepository:IRepositoyBase<Goods>
    {
        void UpdateStokc(int goodsid,int stock);
    }
}
