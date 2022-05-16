using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.IRepository;

namespace Chat.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _iUserRepository;
        public UserController(IUserRepository iUserRepository) {
            _iUserRepository = iUserRepository;
        }

        //用戶頁面
        public async Task<IActionResult> Index(int id)
        {
            var entity= await _iUserRepository.SingleAsync(u=>u.Id==id);
            return View(entity);
        }


    }
}
