using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.DynamicAuth;
using YouTube.AspNetCore.Tutorial.Basic.Filters;
using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [DynamicAuthorization]
    public class AdminController : Controller
    {
        private readonly IGenericService<User, UserListVM, UserCreateVM, UserUpdateVM> _userService;

        public AdminController(IGenericService<User, UserListVM, UserCreateVM, UserUpdateVM> userService)
        {
            _userService = userService;
        }

        public IActionResult UserList()
        {
            var userList = _userService.GetAllItems();
            return View(userList);
        }


        [ServiceFilter(typeof(ParameterCheckFilter<User, UserUpdateVM>))]
        public IActionResult RemoveUser(int id)
        {
            _userService.DeleteItem(id);
            return RedirectToAction("UserList", "Admin");
        }
    }
}
