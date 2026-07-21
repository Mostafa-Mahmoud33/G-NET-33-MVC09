using GymApp.BLL.ViewModels.Account;
using GymApp.DAl.Models;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> login(LoginViewModel model) 
        { 
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if(user is null)
            {
                ModelState.AddModelError("Invalid Login", "Invalid Email Or Password");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController)
                    .Replace("Controller", string.Empty));
            }
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("Invalid Login", "his account is temporarily locked. Try again later.");

            }
            else
            {
                ModelState.AddModelError("Invalid Login", "Invalid Email Or Password");
            }
            return View(model);

        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(login));
        }

        public IActionResult AccessDenied() => View();
    }
}
