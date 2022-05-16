using System;
using System.Collections.Generic;

#nullable disable

namespace Chat.Entity.Models
{
    public partial class GoodsType
    {
        public GoodsType()
        {
            Goods = new HashSet<Good>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}
