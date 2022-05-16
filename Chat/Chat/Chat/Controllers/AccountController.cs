using Chat.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models;

namespace Chat.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly IUserRepository _iUserRepository;

        public AccountController(IUserRepository iUserRepository) {
            _iUserRepository = iUserRepository;
        }

        /// <summary>
        /// 登錄頁面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login() {
            return View();
        }

        /// <summary>
        /// 登錄操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string name,string pwd) {
            var user= await _iUserRepository.SingleAsync(u => u.UserName == name && u.UserPwd == pwd);
            if (user == null) {
                ViewData["LoginError"] = "登錄賬號或密碼錯誤";
                return View();
            }

            return RedirectToAction("index","home");
        }


        /// <summary>
        /// 註冊頁面
        /// </summary>
        /// <returns></returns>
        public IActionResult SignIn() {
            return View();
        }

    }
}
