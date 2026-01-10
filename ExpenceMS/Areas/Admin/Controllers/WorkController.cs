using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class WorkController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}
