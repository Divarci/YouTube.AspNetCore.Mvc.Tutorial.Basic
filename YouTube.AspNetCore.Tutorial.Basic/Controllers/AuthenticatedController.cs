using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM;
using YouTube.AspNetCore.Tutorial.Basic.Services.UserService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [Authorize]
    public class AuthenticatedController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticatedController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = _userService.GetItemById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(UserUpdateVM request)
        {
            _userService.UpdateItem(request);
            return RedirectToAction("Index","Dashboard");
        }

        public IActionResult Logout()
        {
            _userService.SignOut();
            return RedirectToAction("Login", "Authenticate");
        }
    }
}
