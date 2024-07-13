using Microsoft.AspNetCore.Mvc;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers.InvoiceApiController;
public class InvoiceController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
