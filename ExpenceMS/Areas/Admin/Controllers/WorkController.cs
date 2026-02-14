using Expense.Application.Interface;
using Expense.Application.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class WorkController : Controller
    {
        private readonly IWorkData _workdata;

        public WorkController(IWorkData workdata)
        {
            _workdata = workdata;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            var data = await _workdata.getWorkPlan(userId);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string taskInput)
        {

            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var inputData = new WorkPlanDto
            {
                Id = 0,
                IsTrue = 1,
                CreateDate = DateTime.Now,
                ValueType = taskInput,
                CreatedBy = userId,
            };

            var result= await _workdata.SaveWorkPlan(inputData);

            if (result.Status)
            {
                TempData["message"]=result.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromBody] WorkPlanDto model)
        {

            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var inputData = new WorkPlanDto
            {
                Id =model.Id,
                IsTrue = model.IsTrue,
                UpdatedDate = DateTime.Now,
                ValueType = "",
                CreatedBy = userId,
            };

            var result = await _workdata.SaveWorkPlan(inputData);

            return Json(new { success = result.Status, message = result.Message });
        }
    }
    public class WorkDto
    {
        public int Id { get; set; }
        public int IsTrue { get; set; } // or bool IsTrue
    }
}
