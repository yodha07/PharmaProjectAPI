using Microsoft.AspNetCore.Mvc;

namespace PharmaProject.Controllers
{
    public class SaleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Stock()
        {
            return View();
        }
        public IActionResult PrintBill()
        {
            return View();
        }
    }
}
