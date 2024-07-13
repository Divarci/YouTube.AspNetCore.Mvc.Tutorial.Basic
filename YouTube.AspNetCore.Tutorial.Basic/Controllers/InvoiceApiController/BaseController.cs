using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Models.InvoiceApiModels.ResponseDto;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers.InvoiceApiController;

public class BaseController<ControllerName> : Controller where ControllerName : Controller
{
    public IActionResult HandleAction<DTO>(CustomResponseDto<DTO> content) where DTO : class
    {
        var controllerFullName = typeof(ControllerName).Name;
        var controllerName = controllerFullName.Replace("Controller", "");

        switch (content.StatusCode)
        {
            case 200:
                return View(content.Data);                               
            case 201:
            case 204:
                return RedirectToAction("Index", controllerName);
            case 400:
            case 500:
                return RedirectToAction("ApiErrorResult", "Exception", new { content.Errors });
            case 401:
            case 403:
                return RedirectToAction("AccessDenied", "Exception", new { content.Errors });
            case 404:
                return RedirectToAction("NotFoundPage","Exception", new {content.Errors});
            default:
                throw new Exception("Api result error");
        }

    }
}
