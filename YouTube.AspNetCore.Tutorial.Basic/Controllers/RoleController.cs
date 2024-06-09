using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.DynamicAuth;
using YouTube.AspNetCore.Tutorial.Basic.Filters;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.RoleVM;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.UserVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [DynamicAuthorization]
    public class RoleController : Controller
    {
        private readonly IGenericService<Role, RoleListVM, RoleCreateVM, RoleUpdateVM> _roleService;

        public RoleController(IGenericService<Role, RoleListVM, RoleCreateVM, RoleUpdateVM> roleService)
        {
            _roleService = roleService;
        }

        public IActionResult GetAllRoles()
        {
            var roles = _roleService.GetAllItems().ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRole(RoleCreateVM request)
        {
            _roleService.CreateItem(request);
            return RedirectToAction("GetAllRoles", "Role");
        }


        [ServiceFilter(typeof(ParameterCheckFilter<Role, RoleUpdateVM>))]
        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var role = _roleService.GetItemById(id);
            return View(role);
        }


        [ServiceFilter(typeof(ParameterCheckFilter<Role, RoleUpdateVM>))]
        [HttpPost]
        public IActionResult UpdateRole(RoleUpdateVM request)
        {
            _roleService.UpdateItem(request);
            return RedirectToAction("GetAllRoles", "Role");
        }


        [ServiceFilter(typeof(ParameterCheckFilter<Role, RoleUpdateVM>))]
        public IActionResult DeleteRole(int id)
        {
            _roleService.DeleteItem(id);
            return RedirectToAction("GetAllRoles", "Role");

        }
    }
}
