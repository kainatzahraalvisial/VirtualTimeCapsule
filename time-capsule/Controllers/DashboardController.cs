using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using time_capsule.Data;
using time_capsule.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace time_capsule.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DashboardController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // Welcome Page (Index)
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            ViewData["UserFirstName"] = User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
            ViewData["NoFrostedBox"] = true; // Disable frosted glass for welcome page
            ViewData["DashboardSection"] = "Welcome"; // Match _Layout.cshtml
            GenerateNotifications(userId);
            SetUnreadNotifications(userId);
            return View();
        }

        // My Capsules (Index with capsules list)
        [HttpGet]
        public IActionResult MyCapsules()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            ViewData["UserFirstName"] = User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
            ViewData["NoFrostedBox"] = false; // Enable frosted glass
            ViewData["DashboardSection"] = "MyCapsules";
            var capsules = _context.Capsules
                .Where(c => c.UserId == userId)
                .ToList();

            GenerateNotifications(userId);
            SetUnreadNotifications(userId);

            return View(capsules);
        }

        // Create Capsule (GET)
        [HttpGet]
        public IActionResult Create()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            ViewData["UserFirstName"] = User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
            ViewData["NoFrostedBox"] = false;
            ViewData["DashboardSection"] = "CreateCapsules";
            SetUnreadNotifications(userId);
            return View();
        }

        // Create Capsule (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string content, DateTime openDate, IFormFile? image)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            ViewData["UserFirstName"] = User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
            ViewData["NoFrostedBox"] = false;
            ViewData["DashboardSection"] = "CreateCapsules";

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content) || openDate <= DateTime.Now)
            {
                ViewData["ErrorMessage"] = "Please fill all fields correctly. Open date must be in the future.";
                SetUnreadNotifications(userId);
                return View();
            }

            var capsule = new Capsule
            {
                Title = title,
                Content = content,
                CreatedDate = DateTime.Now,
                OpenDate = openDate,
                UserId = userId
            };

            // Handle image upload
            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                capsule.ImagePath = $"/uploads/{fileName}";
            }

            _context.Capsules.Add(capsule);
            _context.SaveChanges();

            return RedirectToAction("MyCapsules");
        }

        // View Capsule
        public IActionResult ViewCapsule(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            ViewData["UserFirstName"] = User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
            ViewData["NoFrostedBox"] = false;
            ViewData["DashboardSection"] = "MyCapsules";

            var capsule = _context.Capsules
                .FirstOrDefault(c => c.Id == id && c.UserId == userId);

            if (capsule == null)
            {
                return NotFound();
            }

            if (capsule.OpenDate.HasValue && DateTime.Now < capsule.OpenDate.Value)
            {
                TempData["ErrorMessage"] = "This capsule is still locked.";
                return RedirectToAction("MyCapsules");
            }

            SetUnreadNotifications(userId);
            return View(capsule);
        }

        // Delete Capsule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            ViewData["UserFirstName"] = User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
            ViewData["NoFrostedBox"] = false;
            ViewData["DashboardSection"] = "MyCapsules";

            var capsule = _context.Capsules
                .FirstOrDefault(c => c.Id == id && c.UserId == userId);

            if (capsule == null)
            {
                return NotFound();
            }

            // Delete associated notifications
            var notifications = _context.Notifications.Where(n => n.CapsuleId == id);
            _context.Notifications.RemoveRange(notifications);

            // Delete the capsule
            _context.Capsules.Remove(capsule);
            _context.SaveChanges();

            return RedirectToAction("MyCapsules");
        }

        // Notifications
        public IActionResult Notifications()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            ViewData["UserFirstName"] = User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
            ViewData["NoFrostedBox"] = false;
            ViewData["DashboardSection"] = "Notifications";

            var notifications = _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();

            // Mark notifications as read
            foreach (var notification in notifications)
            {
                if (!notification.IsRead)
                {
                    notification.IsRead = true;
                }
            }
            _context.SaveChanges();

            ViewData["UnreadNotifications"] = 0; // Reset since all are marked read
            return View(notifications);
        }

        // Profile
        public IActionResult Profile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            ViewData["UserFirstName"] = User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
            ViewData["NoFrostedBox"] = false;
            ViewData["DashboardSection"] = "Profile";

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return RedirectToAction("Logout", "Account"); // Redirect to logout if user not found
            }

            SetUnreadNotifications(userId);
            return View(user);
        }

        // Helper method to generate notifications
        private void GenerateNotifications(int userId)
        {
            var capsules = _context.Capsules
                .Where(c => c.UserId == userId)
                .ToList();

            foreach (var capsule in capsules)
            {
                if (capsule.OpenDate.HasValue)
                {
                    var openDate = capsule.OpenDate.Value;
                    var now = DateTime.Now;
                    var twoDaysBefore = openDate.AddDays(-2);

                    // Check for "2 days before" notification
                    if (now.Date >= twoDaysBefore.Date && now.Date < openDate.Date)
                    {
                        var existingNotification = _context.Notifications
                            .FirstOrDefault(n => n.CapsuleId == capsule.Id && n.Message.Contains("can be opened in 2 days"));
                        if (existingNotification == null)
                        {
                            var notification = new Notification
                            {
                                UserId = userId,
                                CapsuleId = capsule.Id,
                                Message = $"Your capsule '{capsule.Title}' can be opened in 2 days!",
                                IsRead = false,
                                CreatedDate = DateTime.Now
                            };
                            _context.Notifications.Add(notification);
                        }
                    }

                    // Check for "ready" notification
                    if (now.Date >= openDate.Date)
                    {
                        var existingNotification = _context.Notifications
                            .FirstOrDefault(n => n.CapsuleId == capsule.Id && n.Message.Contains("is ready to open"));
                        if (existingNotification == null)
                        {
                            var notification = new Notification
                            {
                                UserId = userId,
                                CapsuleId = capsule.Id,
                                Message = $"Your capsule '{capsule.Title}' is ready to open!",
                                IsRead = false,
                                CreatedDate = DateTime.Now
                            };
                            _context.Notifications.Add(notification);
                        }
                    }
                }
            }
            _context.SaveChanges();
        }

        // Helper method to set unread notification count
        private void SetUnreadNotifications(int userId)
        {
            try
            {
                ViewData["UnreadNotifications"] = _context.Notifications
                    .Count(n => n.UserId == userId && !n.IsRead);
            }
            catch
            {
                ViewData["UnreadNotifications"] = 0; // Fallback in case database isn't ready
            }
        }
    }
}