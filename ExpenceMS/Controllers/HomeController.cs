using ExpenceMS.Models;
using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Expense.Domain.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ExpenceMS.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAuth _auth;
        private readonly string _imagePath;
        public HomeController(IAuth auth, IConfiguration configuration)
        {
            _auth = auth;
            _imagePath = configuration["ImageStorage:TokenImagePath"]
                    ?? throw new ArgumentNullException("ImageStorage:TokenImagePath not configured");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var validUser = await _auth.LoginAsync(model);

            if (validUser.Status) // password hash verify
            {
                // ✅ Save info in session
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, validUser.list.LoginName),
                        new Claim("UserId", validUser.list.UserId.ToString()),
                        new Claim("UserImage", validUser.list.ImageName ?? "/images/default.png")
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.Now.AddHours(8)
                    });

                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            TempData["Error"] = "Invalid username or password";
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
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
