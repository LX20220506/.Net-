using Goods.EF;
using Goods.Entity.Models;
using Goods.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly AuthorityDbContext _authorityDbContext;

        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager, AuthorityDbContext authorityDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _authorityDbContext = authorityDbContext;
        }

        //添加角色
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRole(string rolename) {
            Role role = new Role() {Name=rolename };

            if (await _roleManager.FindByNameAsync(rolename)!=null)
            {
                return Ok(new ApiResult(msg: "已存在该角色",stateCode:404));
            }

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok(new ApiResult(msg: "添加成功"));
            }
            return Ok(new ApiResult(msg: "添加失败", stateCode: 404));
        }

        //删除角色
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRole(string id) {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role==null)
            {
                return BadRequest("不存在该角色"); 
            }
            IList<User> users = await _userManager.GetUsersInRoleAsync(role.Name);

            // 删除所有用户上的角色
            try
            {
                foreach (var user in users)
                {
                    var removeUserRole = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (!removeUserRole.Succeeded)
                    {
                        return Ok(new ApiResult(msg:"删除失败",stateCode:404));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok(new ApiResult(msg:"删除成功"));
            }
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult GetAllRole() {
            var data = _authorityDbContext.Roles.ToList();
            return Ok( new ApiResult(data: data));
        }
    }
}
