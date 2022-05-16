using SqlSugar;

namespace Shool.Entity.Models
{
    public class Admin
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int AdminId { get; set; }

        [SugarColumn(ColumnDataType ="nvarchar(10)")]
        public string AdminName { get; set; }

        [SugarColumn(ColumnDataType ="varchar(64)")]
        public string AdminPwd { get; set; }
    }
}
