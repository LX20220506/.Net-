using Goods.Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Tools.Jwt
{
    public static class JwtHelper
    {
        public static async Task<string> GetToken(UserManager<User> userManager,User user,string key) {
            // 添加权限
            List<Claim> claims = new List<Claim>();

            // 添加用户名
            claims.Add(new Claim(ClaimTypes.Name,user.UserName));

            // 添加角色
            var roleList = await userManager.GetRolesAsync(user);

            foreach (var item in roleList)
            {
                claims.Add(new Claim(ClaimTypes.Role,item));
            }

            // 加密key
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // 配置token
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: "server",// 发行者
                audience: "client",// 订阅者
                claims: claims,// 信息
                signingCredentials: new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256),
                notBefore: DateTime.Now,// 存在时间
                expires: DateTime.Now.AddMinutes(3) // 到期时间
                );

            // 输出token
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        }
    }
}
