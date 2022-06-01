using System;
using System.Collections.Generic;

namespace Demo.BingFa.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public int  UserId { get; set; }
        public int GoodsId { get; set; }
        public DateTime CreateTime { get; set; }

        public int State { get; set; }

        /// <summary>
        /// 一个订单只能有一个用户 一个用户可以有多个订单
        /// </summary>
        //public User User { get; set; }

        /// <summary>
        /// 一个订单有一个商品  一个商品有多个订单
        /// </summary>
        //public Goods Goods { get; set; } 
    }
}
