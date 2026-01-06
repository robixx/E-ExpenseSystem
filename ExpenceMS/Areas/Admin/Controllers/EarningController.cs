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
    public class EarningController : Controller
    {
        private readonly IIncome _income;
        private readonly IDropdown _dropdown;
        public EarningController(IIncome income, IDropdown dropdown)
        {
            _income = income;
            _dropdown = dropdown;
        }

        [HttpGet]
        public async Task<IActionResult> IncomeList()
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized(); 

            int userId =int.Parse( userIdClaim.Value);
            var list = await _income.GetIncomeAysnc(userId);
            ViewBag.income = list.incomelist;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IncomeList(InComeDto datalist)
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
           
            if (ModelState.IsValid)
            {
                datalist.CreatedBy = userId;
                var result = await _income.SaveIncomeAysnc(datalist);
                TempData["message"]=result.Message;
            }
            

            return RedirectToAction("IncomeList");
        }


        [HttpGet]
        public async Task<IActionResult> BackUpMoney()
        {
            var categories = await _dropdown.getIncomeAsync();
            ViewBag.Category = new SelectList(categories, "Id", "Name");
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            var datalist = await _income.GetBackIncomeAysnc(userId);
            ViewBag.saveMoney = datalist.list;
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> BackUpMoney(BackupMoneyDto model)
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            if (ModelState.IsValid)
            {
                model.CreatedBy = userId;
                var result = await _income.BackIncomeAysnc(model);
                TempData["message"] = result.Message;
            }
            return RedirectToAction("BackUpMoney");
        }
    }
}
