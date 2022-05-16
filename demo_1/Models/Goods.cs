using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Goods
    {
        public int ID { get; set; }
        public string GoodsName { get; set; }
        public decimal Price { get; set; }
        public int KunCun { get; set; }
        public int TypeID { get; set; }
    }
}
