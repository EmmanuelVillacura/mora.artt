using Microsoft.AspNetCore.Mvc;

namespace webbappemma.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult contact()
        {
            return View();
        }
    }
}