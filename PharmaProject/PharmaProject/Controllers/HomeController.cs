using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PharmaProject.Models;

namespace PharmaProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Error(string msg)
        {
            ViewData["Error"] = msg;
            return View("Error");
        }
    }
}
