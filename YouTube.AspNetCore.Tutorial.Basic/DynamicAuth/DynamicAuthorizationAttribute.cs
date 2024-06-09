using Microsoft.AspNetCore.Mvc;

namespace YouTube.AspNetCore.Tutorial.Basic.DynamicAuth
{
    public class DynamicAuthorizationAttribute : TypeFilterAttribute
    {
        public DynamicAuthorizationAttribute() : base(typeof(DynamicAuthorizationFilter))
        {
        }
    }
}
