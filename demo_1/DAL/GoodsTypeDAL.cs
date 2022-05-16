using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;


namespace DAL
{
    public static class GoodsTypeDAL
    {
        //查詢所有類型
        public static DataTable GetGoodsTypeInfo() {
            string sql = "select * from goodstype";
            return DBHelper.GetDataTable(sql);
        }

        //添加商品類型
        public static bool AddGoodsTypeInfo(string typename) {
            string sql = $"insert into GoodsType values('{typename}')";
            return DBHelper.GetExecuteNonQuery(sql);
        }

        //刪除商品類型
        public static bool DeleteGoodsTypeInfo(int typeid) {
            string sql = $"delete GoodsType where TypeID={typeid}";
            return DBHelper.GetExecuteNonQuery(sql);
        }

        //修改商品類型信息
        public static bool UpdateGoodsTypeInfo(GoodsType gtype) {
            string sql = $"update GoodsType set TypeName='{gtype.TypeName}' where TypeID={gtype.TypeID}";
            return DBHelper.GetExecuteNonQuery(sql);
        }

    }
}
