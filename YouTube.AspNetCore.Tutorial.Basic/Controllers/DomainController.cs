using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.DynamicAuth;
using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.DomainVM;
using YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.RoleVM;
using YouTube.AspNetCore.Tutorial.Basic.Services;
using YouTube.AspNetCore.Tutorial.Basic.Services.DomainService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [DynamicAuthorization]
    public class DomainController : Controller
    {
        private readonly IDomainService _domainService;
        private readonly IGenericService<Role, RoleListVM, RoleCreateVM, RoleUpdateVM> _roleService;

        public DomainController(IGenericService<Role, RoleListVM, RoleCreateVM, RoleUpdateVM> roleService, IDomainService domainService)
        {
            _roleService = roleService;
            _domainService = domainService;
        }

        public IActionResult GetAllListByRole(int roleId)
        {
            var domainList = _domainService.GetAllDomainsByRoleId(roleId);
            return View(domainList);
        }

        public IActionResult GetAllDomainsByControllerNameId(ControllerParamsVM model)
        {
            var domainListWithRoles = _domainService.GetAllDomainsByControllerNameId(model.ControllerId);
            return View(new DomainListWithParamsVM { DomainList = domainListWithRoles, ControllerParams = model });
        }

        [HttpGet]
        public IActionResult AddDomainRoleToController(ControllerParamsVM model)
        {
            var roleList = _roleService.GetAllItems().ToList();
            return View(new DomainCreateVM
            {
                Roles = roleList,
                ControllerNameId = model.ControllerId,
                ControllerNameForTitle = model.ControllerName
            });
        }

        [HttpPost]
        public IActionResult AddDomainRoleToController(DomainCreateVM request)
        {
            _domainService.CreateItem(request);
            return RedirectToAction("GetAllDomainsByControllerNameId", "Domain", new { controllerId = request.ControllerNameId, controllerName = request.ControllerNameForTitle });
        }

        [HttpGet]
        public IActionResult UpdateDomainRoleForController(ControllerParamsVM model)
        {
            var domainRole = _domainService.GetitemByModel(model);
            var roleList = _roleService.GetAllItems().ToList();
            domainRole.Roles = roleList;
            return View(domainRole);
        }
        [HttpPost]
        public IActionResult UpdateDomainRoleForController(DomainUpdateVM request)
        {
            _domainService.UpdateItem(request);
            return RedirectToAction("GetAllDomainsByControllerNameId", "Domain", new { controllerId = request.ControllerNameId, controllerName = request.ControllerNameForTitle });
        }

        public IActionResult RemoveDomainRoleFromController(ControllerParamsVM model)
        {
            _domainService.DeleteItem(model.Id);
            return RedirectToAction("GetAllDomainsByControllerNameId", "Domain", new { controllerId = model.ControllerId, controllerName = model.ControllerName });
        }
    }
}
