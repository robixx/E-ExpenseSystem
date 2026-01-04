using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EarningController : Controller
    {


        [HttpGet]
        public IActionResult IncomeList()
        {
            return View();
        }
    }
}
