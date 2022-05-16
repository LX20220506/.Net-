using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Models
{
    public class TypesInfo:BaseID
    {
        [SugarColumn(ColumnDataType ="nvarchar(10)")]
        public string TypeName { get; set; }
    }
}
