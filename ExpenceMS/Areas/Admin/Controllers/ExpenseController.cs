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

            var categories = await _dropdown.getCategoryAsync();
            ViewBag.Category = new SelectList(categories, "Id", "Name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ExpenseAdd(ExpenseValueDto expense)
        {

            var categories = await _dropdown.getCategoryAsync();
            ViewBag.Category = new SelectList(categories, "Id", "Name");

            var result= await _expense.ExpenseAysnc(expense);

            if (result.Status)
            {
                return RedirectToAction("ExpenseAdd");
            }

            return View();
        }
    }
}
