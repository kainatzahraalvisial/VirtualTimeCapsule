using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using time_capsule.Models;

namespace time_capsule.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Home";
        ViewData["IsLoggedIn"] = User.Identity?.IsAuthenticated ?? false;
        return View();
    }

    public IActionResult Privacy()
    {
        ViewData["Title"] = "Privacy";
        ViewData["IsLoggedIn"] = User.Identity?.IsAuthenticated ?? false;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ViewData["Title"] = "Error";
        ViewData["IsLoggedIn"] = User.Identity?.IsAuthenticated ?? false;
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}