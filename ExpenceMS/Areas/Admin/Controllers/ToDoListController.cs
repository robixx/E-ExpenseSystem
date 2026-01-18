using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ToDoListController : Controller
    {
        [HttpGet]
        public IActionResult ToDoIndex()
        {
            return View();
        }
    }
}
