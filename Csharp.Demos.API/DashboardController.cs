using Csharp.Demos.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Csharp.Demos.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        [HttpGet]
        [AuthAttribute("Index","admin")]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return Ok("Hurray!! You have an access... Welcome to our World...");

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("v1")]
        public async Task<IActionResult> Version()
        {
            return Ok("Initial version");

        }
    }
}
