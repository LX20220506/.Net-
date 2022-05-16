using System;

namespace Goods.Entity.Models
{
    public class LoginLog
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 登录的用户名称
        /// </summary>
        public string  UserName { get; set; }

        /// <summary>
        /// 登录地址
        /// </summary>
        public string IP { get; set; }


        /// <summary>
        /// 登录的状态
        /// </summary>
        public int LoginState { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

    }
}
