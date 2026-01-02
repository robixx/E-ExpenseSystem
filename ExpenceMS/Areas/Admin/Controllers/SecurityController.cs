using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SecurityController : Controller
    {
        private readonly IUser _user;
        private readonly string _imagePath;
        public SecurityController(IUser user, IConfiguration configuration)
        {
            _user = user;
            _imagePath = configuration["ImageStorage:TokenImagePath"]
                    ?? throw new ArgumentNullException("ImageStorage:TokenImagePath not configured");
        }

        public async Task<IActionResult> UserList()
        {

            var list= await _user.GetUserAsync();

            return View(list.userList);
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            UserDto userlist= new UserDto();
            var GenderList = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Male", Value = "Male" },
                    new SelectListItem { Text = "Female", Value = "Female" },
                    new SelectListItem { Text = "Other", Value = "Other" }
                };
            var Actived = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Active", Value = "1" },
                    new SelectListItem { Text = "In-Active", Value = "2" },
                    
                };

            ViewBag.gender = GenderList;
            ViewBag.Status = Actived;
            return View(userlist);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser( UserDto model, IFormFile image)
        {
            var userlist = await _user.AddUserAsync(model, image);

            return RedirectToAction("UserList");
        }



        [HttpGet("Admin/Security/Image/{fileName}")]
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
    }
}
