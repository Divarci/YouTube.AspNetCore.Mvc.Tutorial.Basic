using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.DynamicAuth;
using YouTube.AspNetCore.Tutorial.Basic.Filters;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM;
using YouTube.AspNetCore.Tutorial.Basic.Services.UserService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [DynamicAuthorization]
    public class AuthenticatedController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticatedController(IUserService userService)
        {
            _userService = userService;
        }


        [ServiceFilter(typeof(ParameterCheckFilter<User, UserUpdateVM>))]
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = _userService.GetItemById(id);
            return View(user);
        }


        [ServiceFilter(typeof(ParameterCheckFilter<User, UserUpdateVM>))]
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
