using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class Program
    {
        /// <summary>
        /// 该例子直接F5运行，自动建库建表
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            OrderService order = new OrderService();

            //建库
            order.AsSugarClient().DbMaintenance.CreateDatabase();
            //建表
            order.AsSugarClient().CodeFirst.InitTables<Order, OrderItem>();

            //调用方法
            order.Insert(new Order() { Name = Guid.NewGuid().ToString() });
            var orderItemList = order.GetOrderItems();
            var orderList=order.GetOrders();
            var orderPage = order.GetOrderPage(it => it.Id > 0, 1, 10);
            var orderJsonList=order.GetOrderByJson("{id:1}");//仓储扩展方法

            Console.WriteLine("执行完成");
            Console.ReadKey();
        }
    }
    public class OrderService : Repository<Order>
    {

        //获取所有订单
        public List<Order> GetOrders()
        {
            return base.GetList(); //使用自已的仓储方法
        }

        //获取所有子订单
        public List<OrderItem> GetOrderItems()
        {
            var orderItemDb = base.Change<OrderItem>();//切换仓仓（新功能）
            return orderItemDb.GetList();
        }

        //分页
        public List<Order> GetOrderPage(Expression<Func<Order, bool>> where, int pagesize, int pageindex) 
        {
            return base.GetPageList(where, new SqlSugar.PageModel() { PageIndex=pageindex,PageSize=pagesize }); //使用自已的仓储方法
        }

        //调用仓储扩展方法
        public List<Order> GetOrderByJson(string Json)
        {
           return base.CommQuery(Json);
        }
    }

    public class Order
    {
        [SqlSugar.SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class OrderItem
    {
        [SqlSugar.SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string ItemName { get; set; }
    }
}
