using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Csharp.Demos.API.Helpers
{
    public class AuthorizeAction : IAuthorizationFilter
    {
        private readonly string _actionName;
        private readonly string _roleType;

        public AuthorizeAction(string actionName, string roleType)
        {
            _actionName = actionName;
            _roleType = roleType;

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string role = context.HttpContext.Request.Headers["role"].ToString();
            switch(_actionName)
            {
                case "Index":
                    if (!role.Contains("admin")) context.Result = new JsonResult("Sorry, You don't have permission to access. Please work with your admin.");
                    break;
            }
        }
    }
}
