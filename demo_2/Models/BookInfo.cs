using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Models
{
    public class BookInfo:BaseID
    {
        [SugarColumn(ColumnDataType ="nvarchar(40)")]
        public string BookName { get; set; }
        [SugarColumn(ColumnDataType ="nvarchar(100)")]
        public string Introduction { get; set; }
        public decimal Price { get; set; }
        public int TypeID { get; set; }
        [SugarColumn(ColumnDataType ="nvarchar(20)")]
        public string Writer { get; set; }

        [SugarColumn(IsIgnore =true)]
        public TypesInfo TypesInfo { get; set; }



    }
}
