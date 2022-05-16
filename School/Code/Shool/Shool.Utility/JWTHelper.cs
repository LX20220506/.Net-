using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shool.Utility
{
    public static class JWTHelper
    {
        public static string GetToken(int id,string name,string userrole)
        {
            //1.创建数组 
            // claim表示信息 (token携带的数据)
            Claim[] claims = {
                new Claim("Id",id.ToString()),
                new Claim("Name",name),
                new Claim(ClaimTypes.Role,"teacher"),

                //定义角色，[Authorize(Roles ="admin")]可以访问；
                //如果使用角色授权的话，ClaimTypes.Role是固定值 不能写成 new Claim("Roles","admin")
                //new Claim(ClaimTypes.Role,"admin")


            };

            //2.创建token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dashk-asdkjakfbj-ADJlsdD78d-Dkd8Dd8"));//秘钥至少要16位

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",//服务器
                audience: "http://localhost:5001",//客户端
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(24),//过期时间
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256) //签名
                );

            //3.生成token
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);


            return jwtToken;
        }
    }
}
