using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppFour.Models.Entrance;
using AppFour.Globals;

namespace AppFour.Controllers
{
    public class EntranceController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public EntranceController(UserManager<User> usrMgr, SignInManager<User> signinMgr)
        {
            userManager = usrMgr;
            signInManager = signinMgr;
        }

        [HttpGet]
        public ViewResult Registration() => View();

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.Name,
                    Email = model.Email,
                    RegistrationDate = DateTime.Now.ToString(),
                    LatestLoginDate = DateTime.Now.ToString(),
                    Status = true
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var res = await userManager.AddToRoleAsync(user, "User");
                    if (res.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public ViewResult Login(string returnUrl = null) 
        {
            return View(new LoginModel { ReturnUrl = returnUrl}); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    bool blockResult = UserProcessing.CheckBlock(user);
                    if (!blockResult)
                    {
                        var result =
                            await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                        if (result.Succeeded)
                        {
                            await userManager.UpdateAsync(UserProcessing.UpdateUser(user));
                            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                            {
                                return Redirect(model.ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Login Failed: Invalid Email and (or) password");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login Failed: This account is blocked!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This account doesn't exists!");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
