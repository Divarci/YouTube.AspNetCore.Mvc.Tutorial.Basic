using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.RoleVM;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserRoleVM;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private readonly IGenericService<UserRole, UserRoleListVM, UserRoleCreateVM, UserRoleUpdateVM> _userRoleService;
        private readonly IGenericService<Role, RoleListVM, RoleCreateVM, RoleUpdateVM> _roleService;
        private readonly IGenericService<User, UserListVM, UserCreateVM, UserUpdateVM> _userService;

        public UserRoleController(IGenericService<UserRole, UserRoleListVM, UserRoleCreateVM, UserRoleUpdateVM> userRoleService, IGenericService<Role, RoleListVM, RoleCreateVM, RoleUpdateVM> roleService, IGenericService<User, UserListVM, UserCreateVM, UserUpdateVM> userService)
        {
            _userRoleService = userRoleService;
            _roleService = roleService;
            _userService = userService;
        }

        public IActionResult GetUserRoles(int userId)
        {
            var allRoles = _roleService.GetAllItems();
            var user = _userService.GetAllItems().FirstOrDefault(x => x.Id == userId);

            List<RoleListVM> roles = new();

            foreach (var role in allRoles)
            {
                if (!user.UserRoles.Any(x => x.RoleId == role.Id))
                {
                    roles.Add(role);
                }
            }
            
            ViewBag.Roles = roles;

            var userRoles = _userRoleService.GetAllItems().Where(x => x.User.Id == userId).ToList();
            return View(userRoles);
        }

        [HttpPost]
        public IActionResult AddRoleToUser(UserRoleCreateVM request)
        {
            _userRoleService.CreateItem(request);
            return RedirectToAction("GetUserRoles", "UserRole", new { userId = request.UserId });
        }

        public IActionResult RemoveRoleFromUser(int id, int userId)
        {
            var user = _userService.GetAllItems().FirstOrDefault(x => x.Id == userId);
            if(user.UserRoles.Count is 1)
            {
                throw new Exception("....");
            }

            _userRoleService.DeleteItem(id);
            return RedirectToAction("GetUserRoles", "UserRole", new { userId });
        }
    }
}
