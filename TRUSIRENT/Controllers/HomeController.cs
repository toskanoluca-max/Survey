using Microsoft.AspNetCore.Mvc;

namespace TRUSIRENT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}