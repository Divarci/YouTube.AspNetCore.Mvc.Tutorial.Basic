using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [Authorize]
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
