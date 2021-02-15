using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Authorize(string response_type, string client_id, string redirect_uri, string scope, string state)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authorize(string username, string password)
        {
            return View();
        }

        public IActionResult Token()
        {
            return View();
        }
    }
}
