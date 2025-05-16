using Microsoft.AspNetCore.Mvc;

namespace time_capsule.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
