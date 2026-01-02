using Expense.Application.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {

        private readonly string _imagePath;
        public DashboardController( IConfiguration configuration)
        {
         
            _imagePath = configuration["ImageStorage:TokenImagePath"]
                    ?? throw new ArgumentNullException("ImageStorage:TokenImagePath not configured");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Admin/Dashboard/Image/{fileName}")]
        public IActionResult Image(string fileName)
        {
            // Build the full folder path
            var folder = Path.Combine(_imagePath);
            var filePath = Path.Combine(folder, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound(); // return 404 if image not found

            // Determine content type based on file extension
            var ext = Path.GetExtension(fileName).ToLower();
            var contentType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };

            // Return the physical file
            return PhysicalFile(filePath, contentType);
        }




        public async Task<IActionResult> Logout()
        {
            // Sign out the user (clears the authentication cookie)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Optional: clear session values
            HttpContext.Session.Clear();

            // Redirect to login page
            return RedirectToAction("Index", "Home", new {Area="Home"});
        }
    }


}
