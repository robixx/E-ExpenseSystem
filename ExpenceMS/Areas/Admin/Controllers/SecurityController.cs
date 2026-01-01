using Expense.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenceMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SecurityController : Controller
    {
        private readonly IUser _user;
        public SecurityController(IUser user)
        {
            _user = user;
        }

        public async Task<IActionResult> UserList()
        {

            var list= await _user.GetUserAsync();

            return View(list.userList);
        }

        public async Task<IActionResult> AddUser()
        {
            return View();
        }
    }
}
