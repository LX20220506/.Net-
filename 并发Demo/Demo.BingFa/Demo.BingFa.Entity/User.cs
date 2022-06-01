using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.BingFa.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string  Phone { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[NotMapped]
        //public List<Order> Orders { get; set; } = new List<Order>();

    }
}
