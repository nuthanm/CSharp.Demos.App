using Microsoft.AspNetCore.Mvc;

namespace Csharp.Demos.API.Helpers
{
    public class AuthAttribute : TypeFilterAttribute
    {
        public AuthAttribute(string actionName, string roleType)
            : base(typeof(AuthorizeAction))
        {
            Arguments = new object[] { actionName, roleType };
        }

    }
}
