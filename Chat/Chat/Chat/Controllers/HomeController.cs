using Chat.IRepository;
using Chat.Models;
using Chat.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository iUserRepository)
        {
            _logger = logger;
            _userRepository = iUserRepository;
        }

        public IActionResult Index() {
            return View();
        }
    }
}
