using Microsoft.AspNetCore.Mvc;

namespace time_capsule.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            // Simulate a logged-in user for testing
            ViewData["IsLoggedIn"] = true;
            ViewData["Title"] = "Dashboard";
            ViewData["UserFirstName"] = "Zahra"; // Placeholder for user data
            return View();
        }

        // Placeholder actions for other dashboard sections
        public IActionResult MyCapsules()
        {
            ViewData["IsLoggedIn"] = true;
            ViewData["Title"] = "Dashboard";
            ViewData["UserFirstName"] = "Zahra";
            return View();
        }

        public IActionResult CreateCapsule()
        {
            ViewData["IsLoggedIn"] = true;
            ViewData["Title"] = "Dashboard";
            ViewData["UserFirstName"] = "Zahra";
            return View();
        }

        public IActionResult Notifications()
        {
            ViewData["IsLoggedIn"] = true;
            ViewData["Title"] = "Dashboard";
            ViewData["UserFirstName"] = "Zahra";
            return View();
        }

        public IActionResult Profile()
        {
            ViewData["IsLoggedIn"] = true;
            ViewData["Title"] = "Dashboard";
            ViewData["UserFirstName"] = "Zahra";
            return View();
        }
    }
}