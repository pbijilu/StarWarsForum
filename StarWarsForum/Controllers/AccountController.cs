using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarWarsForum.Data.Models;
using Microsoft.AspNetCore.Identity;
using StarWarsForum.Models.AccountViewModels;
using StarWarsForum.Services;
using StarWarsForum.Data;
using System;

namespace StarWarsForum.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
       
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            if (TempData["RegisteredSuccessfullyMessage"] != null)
            {
                model.RegisteredSuccessfullyMessage = TempData["RegisteredSuccessfullyMessage"] as string;
            }

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                { 
                    Email = model.Email, 
                    UserName = model.UserName, 
                    BirthDate = model.BirthDate,
                    MemberSince = DateTime.Now,
                    ProfileImageUrl = "https://starwarsforum.blob.core.windows.net/profile-images/avatar.jpg"
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);

                    await _emailService.SendEmailAsync(model.Email, "Account Confirmation",
                        $"Please open this link to confirm your registration on StarWarsForum: <a href='{callbackUrl}'>link</a>");

                    TempData["RegisteredSuccessfullyMessage"] = "You have successfully registered! Please open your email box to verify your account";
                    TempData.Keep("RegisteredSuccessfullyMessage");

                    return RedirectToAction("Register", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var canSignIn = await _signInManager.CanSignInAsync(user) && (user.LockoutEnd < DateTimeOffset.Now || user.LockoutEnabled == false);

            if (canSignIn)
            {
                if (ModelState.IsValid)
                {
                    var result =
                        await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl))
                            return Redirect($"https://{model.ReturnUrl}");
                        else
                            return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect username and(or) password");
                    }
                }
            }
            else
                ModelState.AddModelError("", "Account is banned or email is not verified");
            return View(model);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutPost()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true);

                return RedirectToAction("Index", "Home");
            }
            else
                return View("Error");
        }
    }
}