using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace demo_2.Models
{
    public class BaseBook
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]//主鍵，自增
        public int ID { get; set; }
    }
}
