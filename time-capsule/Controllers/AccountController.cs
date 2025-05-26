using Microsoft.AspNetCore.Mvc;
using time_capsule.Models;
using time_capsule.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace time_capsule.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            ViewData["Title"] = "Signup";
            ViewData["IsLoggedIn"] = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(SignupModel model)
        {
            try
            {
                ViewData["Title"] = "Signup";
                ViewData["IsLoggedIn"] = false;

                Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");
                if (ModelState.IsValid)
                {
                    var existingUser = _context.User.Any(u => u.Email == model.Email);
                    Console.WriteLine($"Email exists: {existingUser}, Email: {model.Email}");
                    if (existingUser)
                    {
                        ModelState.AddModelError("Email", "Email already exists.");
                        return View(model);
                    }

                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    var user = new User
                    {
                        FirstName = model.FirstName ?? string.Empty,
                        LastName = model.LastName ?? string.Empty,
                        Email = model.Email ?? string.Empty,
                        PasswordHash = passwordHash
                    };

                    Console.WriteLine("Adding user to context...");
                    _context.User.Add(user);
                    _context.SaveChanges();
                    Console.WriteLine($"User added. User ID: {user.Id}");

                    var token = GenerateJwtToken(user);
                    Console.WriteLine($"JWT Token generated: {token}");
                    Response.Cookies.Append("JwtToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddDays(1)
                    });

                    ViewData["IsLoggedIn"] = true;
                    Console.WriteLine("Redirecting to Dashboard/Index...");
                    return RedirectToAction("Index", "Dashboard");
                }

                Console.WriteLine("ModelState invalid. Returning signup view.");
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Signup error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                ModelState.AddModelError("", "An error occurred during signup. Please try again.");
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            ViewData["IsLoggedIn"] = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            ViewData["Title"] = "Login";
            ViewData["IsLoggedIn"] = false;

            if (ModelState.IsValid)
            {
                var user = _context.User.FirstOrDefault(u => u.Email == model.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    return View(model);
                }

                var token = GenerateJwtToken(user);
                Response.Cookies.Append("JwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(1)
                });

                ViewData["IsLoggedIn"] = true;
                return RedirectToAction("Index", "Dashboard");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            ViewData["Title"] = "Logout";
            ViewData["IsLoggedIn"] = false;
            Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Index", "Home");
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("DC7385A854722BC4BCBC3B8D38F8B1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds,
                Issuer = "YourIssuer",
                Audience = "YourAudience"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}