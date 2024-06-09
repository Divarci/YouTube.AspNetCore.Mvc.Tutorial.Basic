using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.DynamicAuth;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [DynamicAuthorization]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
