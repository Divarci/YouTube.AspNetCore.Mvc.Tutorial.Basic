using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [Authorize]
    public class UserRoleController : Controller
    {
        public IActionResult GetUserRoles()
        {
            return View();
        }

        public IActionResult AddRoleToUser()
        {
            return View();
        }

        public IActionResult RemoveRoleFromUser()
        {
            return View();
        }
    }
}
