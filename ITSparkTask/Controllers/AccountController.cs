using ITSparkTask.PL.Helpers;
using ITSparkTask.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace ITSparkTask.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = registerVM.Email,
                    UserName = new MailAddress(registerVM.Email).User,    
                };
                var result = await userManager.CreateAsync(user,registerVM.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(registerVM);
                }
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return View(registerVM);
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);
            var user = await userManager.FindByEmailAsync(loginVM.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Email Or Password");
                return View(loginVM);
            }
            var result = await signInManager.PasswordSignInAsync(user, loginVM.Password,loginVM.RemmberMe,false);
            if (result.Succeeded)
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            else
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(ForgetPasswordViewModel forgetPasswordVM)
        {
            if (!ModelState.IsValid)
                return View(nameof(ForgetPassword));
            var user = await userManager.FindByEmailAsync(forgetPasswordVM.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email is Not Correct");
                return View(nameof(ForgetPassword));
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var reseatEmailLink = Url.Action(action: "ReseatPassword", controller: "Account", new { Email = user.Email, Token = token }, Request.Scheme);
            await EmailService.SendEmailAsync(to: user.Email, subject: "Reseat Password", body: reseatEmailLink);
            return RedirectToAction(nameof(CheckYourInbox));
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReseatPassword(string email,string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReseatPassword(ReseatPasswordViewModel reseatPasswordVM)
        {
            if (!ModelState.IsValid)
                return View(reseatPasswordVM);
            var email = TempData["Email"] as string;
            var token = TempData["Token"] as string;
            var user = await userManager.FindByEmailAsync(email);
            var result = await userManager.ResetPasswordAsync(user, token, reseatPasswordVM.Password);
            if (result.Succeeded)
                return RedirectToAction(nameof(Login));
            else
                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, e.Description);
                }
            return View(reseatPasswordVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
           await signInManager.SignOutAsync();
           return RedirectToAction(nameof(Login));
        }
    }
}
