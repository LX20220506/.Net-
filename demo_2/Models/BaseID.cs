using System;
using SqlSugar;

namespace Models
{
    public class BaseID
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int ID { get; set; }
    }
}
