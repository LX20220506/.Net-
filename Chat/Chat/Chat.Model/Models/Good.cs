using System;
using System.Collections.Generic;

#nullable disable

namespace Chat.Entity.Models
{
    public partial class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int KuCun { get; set; }
        public int TypeId { get; set; }

        public virtual GoodsType Type { get; set; }
    }
}
