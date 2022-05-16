using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.Encodings;

namespace Shool.Utility
{
    public static class MD5Helper
    {
        public static string MD5Encrypt64(string pwd) {
            if (pwd!=null&&pwd!="")
            {
                //创建md5对象
                using MD5 md5 = MD5.Create();

                //将密码转换成 byte数组   然后通过md5加密组成一个新的byte数组
                byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));

                return Convert.ToBase64String(s);
            }
            return "";
        }
    }
}
