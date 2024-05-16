using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM;
using YouTube.AspNetCore.Tutorial.Basic.Services.UserService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {            
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserCreateVM request)
        {
            _userService.CreateItem(request);
            return RedirectToAction(nameof(Login));
        }




        public IActionResult Login()
        {
            return View();
        }
    }
}
