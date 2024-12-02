using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApp.Identity._1._0.ConfigIdentity;
using WebApp.Identity._1._0.Models;

namespace WebApp.Identity._1._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<MyUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<MyUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
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
                if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));
                    return RedirectToAction("About");
                }
                ModelState.AddModelError("", "Usuário ou senha invalido");

                return View("Sucess");
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
                if (user == null) {
                    user = new MyUser()
                    {
                        Id= Guid.NewGuid().ToString(),
                        UserName = model.UserName,
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);


                }

                return View("Sucess");
            }

            return View();
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
    }
}
