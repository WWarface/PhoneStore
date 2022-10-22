using Microsoft.AspNetCore.Mvc;

namespace WebServicesProject.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Brand()
        {
            return View();
        }

        public IActionResult Specials()
        {
            return View();
        }

        public IActionResult Contact()
        {      
            return View();
        }
    }
}
