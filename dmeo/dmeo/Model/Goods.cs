//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace dmeo.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Goods
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int KuCun { get; set; }
        public int TypeID { get; set; }
    
        public virtual GoodsType GoodsType { get; set; }
    }
}
