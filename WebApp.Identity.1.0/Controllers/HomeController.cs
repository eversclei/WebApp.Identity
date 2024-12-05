using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApp.Identity._1._0.ConfigIdentity;
using WebApp.Identity._1._0.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Identity._1._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<MyUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<MyUser> _userClaimsPrincipalFactory;

        public HomeController(ILogger<HomeController> logger, UserManager<MyUser> userManager,
            IUserClaimsPrincipalFactory<MyUser> userClaimsPrincipalFactory)
        {
            _logger = logger;
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Sucess()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null && !await _userManager.IsLockedOutAsync(user))
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        if (!await _userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "E-mail is not valid");
                            return View();
                        }
                        await _userManager.ResetAccessFailedCountAsync(user);
                        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
                        await HttpContext.SignInAsync("Identity.Application", principal);
                        return RedirectToAction("About");
                    }
                    await _userManager.AccessFailedAsync(user);

                    if(await _userManager.IsLockedOutAsync(user))
                    {
                        // enviar email sobre troca de senha caso nao for quem tenha feito a tetntaiva
                    }
                }
                ModelState.AddModelError("", "Usuário ou senha invalido");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = new MyUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName,
                        Email = model.UserName
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationUrl = Url.Action("ConfirmEmailAddress", "Home", new { token = token, email = user.Email }, Request.Scheme);


                        System.IO.File.WriteAllText("resetLink.txt", confirmationUrl);
                        return View("Sucess");
                    }
                }

                return View("Sucess");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {

                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (!result.Succeeded)
                {
                    return View("Error");
                }
                return View("Sucess");
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }
            return View("Error");
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetUrl = Url.Action("ResetPassword", "Home", new { token = token, email = model.Email }, Request.Scheme);

                    System.IO.File.WriteAllText("resetLink.txt", resetUrl);
                    return View("Sucess");
                }
                else
                {
                    ModelState.AddModelError("", "User not found");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            return View(new ResetPasswordModel { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {

                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }
                    return View("Sucess");
                }
                else
                {
                    ModelState.AddModelError("", "User not found");
                }
            }
            return View();
        }
    }
}
