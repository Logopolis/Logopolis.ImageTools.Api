using Microsoft.AspNetCore.Mvc;

namespace Logopolis.ImageTools.Api.Controllers
{
    [ApiController]
    public class PingController : ControllerBase
    {
        [Route("ping")]
        [HttpGet]
        public IActionResult Index()
        {
            return new OkObjectResult("Hello from Logopolis Image Tools");
        }
    }
}
