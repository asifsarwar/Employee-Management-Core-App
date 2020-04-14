using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementCoreApp.Helper.Database;
using EmployeeManagementCoreApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeManagementCoreApp.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<ActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id {roleId} cannot be found";
                return View("NotFound");
            }
            RoleEditViewModel model = new RoleEditViewModel()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            List<string> Users = new List<string>();
            foreach (var user in userManager.Users)
            {
                var result = await userManager.IsInRoleAsync(user, role.Name);
                if (result)
                {
                    Users.Add(user.UserName);
                }
            }
            model.Users = Users;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id {model.RoleId} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var model = new List<UserEditViewModel>();
            var role = await roleManager.FindByIdAsync(roleId);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id {roleId} is not found";
                View("NotFound");
            }
            else
            {
                var users = userManager.Users;     
                foreach (var user in users)
                {
                    var userEditViewModel = new UserEditViewModel()
                    {
                        UserName = user.UserName,
                        IsSelected = false,
                        UserId = user.Id
                    };
                    if (await userManager.IsInRoleAsync(user, role.Name)) 
                    {
                        userEditViewModel.IsSelected = true;
                    }
                    else
                    {
                        userEditViewModel.IsSelected = false;
                    }
                    model.Add(userEditViewModel);
                }               
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserEditViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id {roleId} is not found";
                View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                } else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                   result = await  userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if(i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { roleId = roleId });

                    }
                }
            }
            return RedirectToAction("EditRole", new { roleId = roleId });
            
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var model = roleManager.Roles;
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public ViewResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    City = model.City
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
        [HttpGet]
        public ActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }
        [HttpGet]
        public async Task<ActionResult> EditUser(string id)
        {
            var result = await userManager.FindByIdAsync(id);
            if(result == null)
            {
                ViewBag.ErrorMessage = $"User with user Id {id} cannot be found";
                return View("NotFound");
            }
            var userClaims = await userManager.GetClaimsAsync(result);
            var userRoles = await userManager.GetRolesAsync(result);
            EditUserViewModel model = new EditUserViewModel()
            {
                Id = result.Id,
                Username = result.UserName,
                Email = result.Email,
                City = result.City,
                Roles = userRoles.ToList(),
                Claims = userClaims.Select(x=> x.Value).ToList<string>()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with user Id {model.Id} cannot be found";
                return View("NotFound");
            }
            user.Email = model.Email;
            user.UserName = model.Username;
            user.City = model.City;
            var update = await userManager.UpdateAsync(user);
            if (update.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }
            else
            {
                foreach (var error in update.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult CancelEditUser()
        {
            return RedirectToAction("ListUsers");
        }
        [HttpPost]
        public async Task< ActionResult> DeleteUser(string id)
        {
           var user = await  userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with user Id {id} cannot be found";
                return View("NotFound");
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with role Id {id} cannot be found";
                return View("NotFound");
            }
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}