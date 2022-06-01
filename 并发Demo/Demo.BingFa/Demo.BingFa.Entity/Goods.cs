using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.BingFa.Entity
{
    public class Goods
    {
        public int Id { get; set; }
        public string GoodsNmae { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[NotMapped]
        //public List<Order> Order { get; set; } = new List<Order>();
    }
}
