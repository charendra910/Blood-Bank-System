using Blood_Bank_System.Models;
using Blood_Bank_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blood_Bank_System.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<UsersModel> signInManager;
        private readonly UserManager<UsersModel> userManager;

        public AdminController(SignInManager<UsersModel> signInManager, UserManager<UsersModel> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            ViewBag.LoaderDuration = 2000; // 3 seconds
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.RememberMe)
                {
                    ModelState.AddModelError("", "Please check 'Remember Me' to proceed.");
                    return View(model);
                }

                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var claims = await userManager.GetClaimsAsync(user);
                        if (!claims.Any(c => c.Type == "FullName"))
                        {
                            await userManager.AddClaimAsync(user, new Claim("FullName", user.FullName ?? string.Empty));
                            await signInManager.RefreshSignInAsync(user);
                        }

                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email or password is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User not found.");
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UsersModel users = new UsersModel
                {
                    FullName = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await userManager.CreateAsync(users, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Something is wrong!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Admin", new { username = user.UserName });
                }
            }
            return View(model);
        }

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Admin");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, model.NewPassword);
                        return RedirectToAction("Login", "Admin");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. try again.");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Profile()
        {
            var users = userManager.Users.ToList(); // Fetch all users
            return View(users);
        }

        public async Task<IActionResult> ProfileView()
        {
            var users = userManager.Users.ToList(); // Fetch all users
            return View(users);
        }
    }
}


