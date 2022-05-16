using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Models
{
    public class Admin:BaseID
    {
        [SugarColumn(ColumnDataType ="nvarchar(20)")]
        public string AdminName { get; set; }
        [SugarColumn(ColumnDataType ="nvarchar(16)")]
        public string AdminPwd { get; set; }
    }
}
