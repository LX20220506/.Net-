using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Config
    {
        private static string GetCurrentProjectPath
        {

            get
            {
                return Environment.CurrentDirectory.Replace(@"\bin\Debug", "");//获取具体路径
            }
        }
        public static string ConnectionString = @"DataSource=" + GetCurrentProjectPath + @"\DataBase\SqlSugar4xTest.sqlite";
    }
}
