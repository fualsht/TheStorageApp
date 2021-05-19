using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Controllers
{
    //[Route("home")]
    public class HomeController : Controller
    {
        //[Route("index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
