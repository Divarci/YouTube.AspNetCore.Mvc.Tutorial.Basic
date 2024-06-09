using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using YouTube.AspNetCore.Tutorial.Basic.DynamicAuth;
using YouTube.AspNetCore.Tutorial.Basic.Services.ControllerNameService;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [DynamicAuthorization]
    public class ControllerNameController : Controller
    {
        private readonly IControllerNameService _controllerNameService;

        public ControllerNameController(IControllerNameService controllerNameService)
        {
            _controllerNameService = controllerNameService;
        }

        public IActionResult GetAllControllerList()
        {
            var controllerList = _controllerNameService.GetAllItems().ToList();
            return View(controllerList);
        }

        public IActionResult RefreshList()
        {
            _controllerNameService.RefreshControllers();
            return RedirectToAction("GetAllControllerList", "ControllerName");
        }
    }
}
