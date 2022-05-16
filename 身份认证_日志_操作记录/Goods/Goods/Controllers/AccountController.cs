using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goods.Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Goods.Entity.Models;
using Goods.EF;
using Goods.Tools.Jwt;
using Microsoft.Extensions.Configuration;
using Goods.Tools;

namespace Goods.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly AuthorityDbContext _authorityDbContext;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, AuthorityDbContext authorityDbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _authorityDbContext = authorityDbContext;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViweModel viweModel) {
            var user = await _userManager.FindByNameAsync(viweModel.Account);

            if (user==null||user.PasswordHash!=viweModel.Pwd)
            {
                return Ok(new ApiResult(msg:"账号或密码错误",stateCode:404));
            }

            string token = await JwtHelper.GetToken(_userManager, user, _configuration.GetSection("JwtConfig").GetSection("Key").Value);
            return Ok(new ApiResult(data:token));
        }
    }
}
