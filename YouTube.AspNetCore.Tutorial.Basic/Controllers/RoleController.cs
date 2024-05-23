using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YouTube.AspNetCore.Tutorial.Basic.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        //add all crud operations       
    }
}
