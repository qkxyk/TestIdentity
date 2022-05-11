using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System.Diagnostics;

namespace MvcMovie.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize]
        //[Authorize(Policy = "Claim.Count")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetJwtMessage()
        {
            return Content("from jwt authentication");
        }
        public IActionResult GetJwtMessageUnAuthentication()
        {
            return Content("from jwt unauthentication");
        }
        //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme+","+CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(AuthenticationSchemes =$"{JwtBearerDefaults.AuthenticationScheme},{CookieAuthenticationDefaults.AuthenticationScheme}")]
        public IActionResult GetJwtAndCookie()
        {
            return Content("From cookie and jwt Message");
        }
    }
}