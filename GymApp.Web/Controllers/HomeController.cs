using GymApp.BLL.ViewModels.AnalyticsViewModels;
using GymApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymApp.Web.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        
        public IActionResult Index()
        {
            var model = new AnalyticsViewModel
            {
                TotalMembers = 100,
                ActiveMembers = 85,
                TotalTrainers = 20,
                UpcomingSession = 3,
                OngoingSession = 5,
                CompletedSession = 20
            };

            return View(model);
        }
        
        [Authorize("Admin")]
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
