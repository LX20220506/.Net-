using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Entity.Models
{
    public class GoodsInfo
    {
        public int Id { get; private set; }
        public string  GoodsName { get; set; }
        public double GoodsPrice { get; set; }
        public string GoodsDesc { get; set; }

        // 给EF使用
        private GoodsInfo() { }

        public GoodsInfo(string name, double price, string desc) {
            this.GoodsName = name;
            this.GoodsPrice = price;
            this.GoodsDesc = desc;
        }
    }
}
