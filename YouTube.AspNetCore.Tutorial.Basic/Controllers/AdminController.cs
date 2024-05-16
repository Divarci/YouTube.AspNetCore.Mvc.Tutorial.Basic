using Microsoft.AspNetCore.Mvc;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult UserList()
        {
            return View();
        }

        public IActionResult RemoveUser()
        {
            return View();
        }
    }
}
