using Microsoft.AspNetCore.Mvc;

namespace time_capsule.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
