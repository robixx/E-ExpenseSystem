using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EarningController : Controller
    {
        private readonly IIncome _income;
        public EarningController(IIncome income)
        {
            _income = income;
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
            return View();  
        }
    }
}
