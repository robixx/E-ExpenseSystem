using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized(); // or RedirectToAction("Login", "Account");

            var userId = userIdClaim.Value;

            var category_list = await _category.CategoryListAsync(userId);
            return View(category_list.datalist);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryAdd()
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized(); // or RedirectToAction("Login", "Account");

           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAdd(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst("UserId");

                if (userIdClaim == null)
                    return Unauthorized();

                model.CreateBy = int.Parse(userIdClaim.Value);
                var list = await _category.AddCategoryAsync(model);
                if (list.Status)
                {
                    TempData["message"]=list.Message;
                }
                return RedirectToAction("CategoryList");
            }
            return View();
        }
    }
}
