using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;

namespace DAL
{
    public static class GoodsDAL
    {
        //获取全部商品信息
        static public DataTable GetAllGoodsInfo()
        {
            string sql = "select * from goods g,GoodsType t where g.TypeID=t.TypeID";
            return DBHelper.GetDataTable(sql);
        }

        //獲取單個商品信息
        static public DataTable GetGoodsInfoByID(int id) {
            string sql = $"select * from goods where ID={id}";
            return DBHelper.GetDataTable(sql);
        }

        //根据条件查询
        static public DataTable GetLikeGoodsInfo(string name, decimal maxprice, decimal minprice, int typeid)
        {
            string sql = $"select * from Goods where Goods.Name like '%{name}%' and Price>={minprice} and Price<={maxprice} and TypeID={typeid}";
            return DBHelper.GetDataTable(sql);
        }

        //添加商品信息
        static public bool AddGoodsInfo(Goods goods)
        {
            string sql = $"insert Goods values('{goods.GoodsName}',{goods.Price},{goods.KunCun},{goods.TypeID})";
            return DBHelper.GetExecuteNonQuery(sql);
        }

        //删除单个商品信息
        static public bool DeletGoodsInfo(int id)
        {
            string sql = $"delete Goods where ID={id}";
            return DBHelper.GetExecuteNonQuery(sql);
        }

        //删除多个商品信息
        static public bool DeleteGoodsInfoList(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                string sql = $"delete Goods where ID={nums[i]}";
                if (!DBHelper.GetExecuteNonQuery(sql))
                {
                    return false;
                }
            }
            return true;
        }

        //修改商品信息
        static public bool UpdateGoodsInfo(Goods goods)
        {
            string sql = $"update Goods set Name='{goods.GoodsName}',Price={goods.Price},KuCun={goods.KunCun},TypeID={goods.TypeID} where ID={goods.ID}";
            return DBHelper.GetExecuteNonQuery(sql);
        }
    }
}
