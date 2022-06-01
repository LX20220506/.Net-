using Demo.BingFa.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.BingFa.HttpApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(string userName, string pwd, string phone, string email) {
            await _userService.CreateUserAsync(userName,pwd,phone,email);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AutoCrateUser() {
            for (int i = 2; i < 101; i++)
            {
                string data = i.ToString();
                await _userService.CreateUserAsync(data,data, data, data);
            }
            return Ok();
        }

    }
}
