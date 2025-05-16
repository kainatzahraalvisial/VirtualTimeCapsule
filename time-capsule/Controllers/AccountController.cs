using Microsoft.AspNetCore.Mvc;
using time_capsule.Models;

namespace time_capsule.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Signup()
        {
            ViewData["Title"] = "Signup";
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                // Save logic (placeholder for now)
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Authentication logic (placeholder for now)
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
