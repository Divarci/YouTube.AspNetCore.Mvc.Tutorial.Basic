using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YouTube.AspNetCore.Tutorial.Basic.Exceptions;
using YouTube.AspNetCore.Tutorial.Basic.Models.Others;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    public class ExceptionController : Controller
    {
        public IActionResult HandleExceptions()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

            if (exception is ClientSideExceptions)
            {
                return View(new ErrorVM
                {
                    StatusCode = 401,
                    Errors = new List<string> { exception.Message }
                });
            }
            if (exception is ServerSideExceptions)
            {
                return View(new ErrorVM
                {
                    StatusCode = 500,
                    Errors = new List<string> { exception.Message }
                });
            }

            if (exception is UserServiceExceptions)
            {
                return View(new ErrorVM
                {
                    StatusCode = 400,
                    Errors = new List<string> { exception.Message }
                });
            }


            return View(new ErrorVM
            {
                StatusCode = 500,
                Errors = new List<string> { "Inernal Server Error. Please speak with your advisor" }
            });
        }



        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}

// 1-we will get our exceptions

// 2-we will get our not found page