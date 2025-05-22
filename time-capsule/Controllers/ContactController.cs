using Microsoft.AspNetCore.Mvc;
using time_capsule.Models;
using time_capsule.Data;

namespace time_capsule.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Contact";
            ViewData["IsLoggedIn"] = User.Identity?.IsAuthenticated ?? false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactMessage model)
        {
            ViewData["Title"] = "Contact";
            ViewData["IsLoggedIn"] = User.Identity?.IsAuthenticated ?? false;

            if (ModelState.IsValid)
            {
                model.SentDate = DateTime.UtcNow;
                _context.ContactMessages.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}