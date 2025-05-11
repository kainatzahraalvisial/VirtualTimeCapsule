using Microsoft.AspNetCore.Mvc;

namespace time_capsule.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
