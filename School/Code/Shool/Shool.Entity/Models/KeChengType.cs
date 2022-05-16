using SqlSugar;

namespace Shool.Entity.Models
{
    public class KeChengType
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int KeChengTypeId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar(10)")]
        public string KeChengTypeName { get; set; }
    }
}
