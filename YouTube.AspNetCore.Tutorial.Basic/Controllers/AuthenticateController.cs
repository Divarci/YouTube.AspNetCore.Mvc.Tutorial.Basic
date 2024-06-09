using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.DynamicAuth;
using YouTube.AspNetCore.Tutorial.Basic.Models.Others;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM;
using YouTube.AspNetCore.Tutorial.Basic.Services.UserService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [DynamicAuthorization]
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



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM request)
        {
            var result = _userService.SignIn(request.Email, request.Password);
            if (!result)
            {
                ViewBag.Error = "Username or Password is incorrect !!";
                return View();
            }
            return RedirectToAction("Index","Dashboard");
        }
    }
}
