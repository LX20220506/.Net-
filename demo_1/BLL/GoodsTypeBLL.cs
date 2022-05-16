using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Models;
using DAL;

namespace BLL
{
    public class GoodsTypeBLL
    {
        /// <summary>
        /// 查詢所有類型
        /// </summary>
        /// <returns></returns>
        public DataTable GetGoodsInfo() {
            return GoodsTypeDAL.GetGoodsTypeInfo();
        }

        /// <summary>
        /// 刪除類型信息
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public bool DeleteGoodsInfo(int typeid) {
            return GoodsTypeDAL.DeleteGoodsTypeInfo(typeid);
        }

        /// <summary>
        /// 添加商品信息
        /// </summary>
        /// <param name="typename"></param>
        /// <returns></returns>
        public bool AddGoodsTypeInfo(string typename) {
            return GoodsTypeDAL.AddGoodsTypeInfo(typename);
        }

        /// <summary>
        /// 修改類型信息
        /// </summary>
        /// <param name="gtype"></param>
        /// <returns></returns>
        public bool UpdateGoodsTypeInfo(GoodsType gtype) {
            return GoodsTypeDAL.UpdateGoodsTypeInfo(gtype);
        }
    }
}
