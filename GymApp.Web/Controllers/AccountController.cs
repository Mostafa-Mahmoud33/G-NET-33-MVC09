using GymApp.BLL.ViewModels.Account;
using GymApp.DAl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Web.Controllers
{
    public class AccountController(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, 
        ILogger<AccountController> logger)
        : Controller
    {
        [HttpGet]
        public IActionResult login() { return View(); }

        [HttpPost]
        public IActionResult login(LoginViewModel viewModel) 
        { 
            return View(); 
        }
    }
}
