using Microsoft.AspNetCore.Mvc;
using SaarSystem.Models;
using System.Diagnostics;

namespace SaarSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}