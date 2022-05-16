using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
using System.Data;

namespace BLL
{
    public class GoodsBLL
    {
        /// <summary>
        /// 獲取所有信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetGoodsInfo() {
            return GoodsDAL.GetAllGoodsInfo();
        }

        /// <summary>
        /// 查詢單個商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetGoodsInfoByID(int id) {
            return GoodsDAL.GetGoodsInfoByID(id);
        }

        /// <summary>
        /// 根據條件查詢信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="maxprice"></param>
        /// <param name="minprice"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public DataTable GetLikeGoodsInfo(string name,decimal maxprice,decimal minprice,int typeid) {
            return GoodsDAL.GetLikeGoodsInfo(name,maxprice,minprice,typeid);
        }

        /// <summary>
        /// 添加商品信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public bool AddGoodsInfo(Goods goods) {
            return GoodsDAL.AddGoodsInfo(goods);
        }

        /// <summary>
        /// 刪除單個商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteGoodsInfo(int id) {
            return GoodsDAL.DeletGoodsInfo(id);
        }

        /// <summary>
        /// 批量刪除商品信息
        /// </summary>
        /// <param name="nums">商品id數組</param>
        /// <returns></returns>
        public bool DeleteGoodsInfoList(int[] nums) {
            return GoodsDAL.DeleteGoodsInfoList(nums);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public bool UpdateGoodsInfo(Goods goods) {
            return GoodsDAL.UpdateGoodsInfo(goods);
        }

    }
}
