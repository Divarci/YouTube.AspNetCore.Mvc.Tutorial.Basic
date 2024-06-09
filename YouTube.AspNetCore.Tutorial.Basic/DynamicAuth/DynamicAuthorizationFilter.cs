using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YouTube.AspNetCore.Tutorial.Basic.Services.DomainService;

namespace YouTube.AspNetCore.Tutorial.Basic.DynamicAuth
{
    public class DynamicAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IDomainService _domainService;

        public DynamicAuthorizationFilter(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var controllerName = context.RouteData.Values["controller"]?.ToString();

            var controllerDomains = _domainService.GetAllDomainsByControllerName(controllerName!);

            var permissonCounter = 0;

            var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;

            if (!isAuthenticated)
            {
                if (!controllerDomains.Any(x => x.Role.RoleName == "Public"))
                {
                    context.Result = new RedirectToActionResult("Login", "Authenticate", new { error = "Please authenticate first" });
                }
            }

            if (isAuthenticated)
            {
                foreach (var domain in controllerDomains)
                {
                    var hasPermission = context.HttpContext.User.IsInRole(domain.Role.RoleName) || domain.Role.RoleName == "Public";
                    if (hasPermission)
                    {
                        permissonCounter++;
                    }
                }

                if(permissonCounter <= 0)
                {
                    context.Result = new ForbidResult();
                }
            }

        }
    }
}
