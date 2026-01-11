using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IDropdown _dropdown;
        private readonly IExpense _expense;
        public ExpenseController(IDropdown dropdown,IExpense expense)
        {
            _dropdown = dropdown;
            _expense = expense;
        }


        [HttpGet]
        public async Task<IActionResult> ExpenseAdd()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                return Unauthorized();
            int userId= int.Parse(userIdClaim.Value);
            var categories = await _dropdown.getCategoryAsync();
            ViewBag.Category = new SelectList(categories, "Id", "Name");
            var data = await _expense.GetExpenseAysnc(userId);
            ViewBag.expense = data.list;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ExpenseAdd(ExpenseValueDto expense)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);
            var categories = await _dropdown.getCategoryAsync();
            ViewBag.Category = new SelectList(categories, "Id", "Name");

            var result= await _expense.ExpenseAysnc(expense, userId);

            if (result.Status)
            {
                TempData["message"] = result.Message;
                return RedirectToAction("ExpenseAdd");
            }

            return View();
        }
    }
}
