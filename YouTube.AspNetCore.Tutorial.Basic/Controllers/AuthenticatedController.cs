using Microsoft.AspNetCore.Mvc;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    public class AuthenticatedController : Controller
    {
        public IActionResult EditUser()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
