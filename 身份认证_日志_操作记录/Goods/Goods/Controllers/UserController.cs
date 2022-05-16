using Goods.EF;
using Goods.Entity.Models;
using Goods.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AuthorityDbContext _authorityDbContext;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager, AuthorityDbContext authorityDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authorityDbContext = authorityDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string name,string pwd) {
            if (pwd.Length<6)
            {
                return Ok(new ApiResult(msg: "密码长不够",stateCode:404));
            }
            User user = new User() {UserName=name,PasswordHash=pwd };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new ApiResult(msg: "添加成功"));
            }
            return Ok(new ApiResult(msg: "添加失败",stateCode:500));
        }

        [HttpDelete]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> DeleteUser(string id) {
            User user= await _userManager.FindByIdAsync(id);
            if (user==null)
            {
                return Ok(new ApiResult(msg: "用户不存在",stateCode:404));
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new ApiResult(msg: "删除成功"));
            }
            return Ok(new ApiResult(msg: "删除失败", stateCode: 500));
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> FindUser(string id) {
            User user = await _userManager.FindByIdAsync(id);

            if (user==null)
            {
                return Ok(new ApiResult(msg: "未找到指定用户",stateCode:404));
            }

            return user;
        }

        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> ChangePassword(string id,string oldPwd,string newPwd) {
            if (oldPwd.Length<6||newPwd.Length<6)
            {
                return Ok(new ApiResult(msg: "您的密码长度不够",stateCode:404));
            }
            User user = await _userManager.FindByIdAsync(id);
            if (user==null)
            {
                return Ok(new ApiResult(msg: "未找到指定用户", stateCode: 404));
            }
            
            if (user.PasswordHash == oldPwd)
            {
                user.PasswordHash = newPwd;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(new ApiResult(msg: "修改成功"));
                }
            }
            else
            {
                return Ok(new ApiResult(msg: "旧密码不正确", stateCode: 404));
            }

            return Ok(new ApiResult(msg: "修改失败", stateCode: 500));
        }

        //给用户添加角色
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddUserRole(string userid, string roleid) {
            User user = await _userManager.FindByIdAsync(userid);
            if (user==null)
            {
                return Ok(new ApiResult(msg: "不存在该用户",stateCode:404));
            }

            Role role = await _roleManager.FindByIdAsync(roleid);
            if (role==null)
            {
                return Ok(new ApiResult(msg: "不存在该用户",stateCode:404));
            }

            var result = await _userManager.AddToRoleAsync(user,role.Name);
            if (result.Succeeded)
            {
                return Ok(new ApiResult(msg: "添加成功"));
            }
            return Ok(new ApiResult(msg: "添加失败", stateCode: 500));

        }


        //删除用户角色
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUserRole(string userid, string roleid) {
            User user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return Ok(new ApiResult(msg: "不存在该用户", stateCode: 404));
            }

            Role role = await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return Ok(new ApiResult(msg: "不存在该角色", stateCode: 404));
            }

            var result =  await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return Ok(new ApiResult(msg: $"已将{user.UserName}的{role.Name}角色删除"));
            }

            return Ok(new ApiResult(msg: "删除失败", stateCode: 500));
        }

        // 获取用户的所有角色
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserRoles(string userid) {

            var user = await _userManager.FindByIdAsync(userid);

            if (user==null)
            {
                return Ok(new ApiResult(msg: "没有找到该用户",stateCode:404));
            }

            var list = await _userManager.GetRolesAsync(user);
            return Ok(new ApiResult(data: list));
        }

        // 获取所有用户
        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult GetAllUser() {
            var users = _authorityDbContext.Users.ToList();
            return Ok(new ApiResult(data: users));

        }

    }
}
