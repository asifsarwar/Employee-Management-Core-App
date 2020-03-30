using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementCoreApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementCoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            if (signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpGet]
        public ViewResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index","Home");
                }
                foreach(var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
            }
            return View(model);
        }
    }
}