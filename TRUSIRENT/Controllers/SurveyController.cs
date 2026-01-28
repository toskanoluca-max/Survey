using Microsoft.AspNetCore.Mvc;

namespace TRUSIRENT.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}