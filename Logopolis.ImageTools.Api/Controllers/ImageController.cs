using Microsoft.AspNetCore.Mvc;

using Logopolis.ImageTools.ImageProcessing.Domain.Commands;
using Logopolis.ImageTools.ImageProcessing.Domain.ServiceInterfaces;
using Logopolis.ImageTools.Api.MvcExtensions;

namespace Logopolis.ImageTools.Api.Controllers
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IResizeImageCommandService _resizeImageCommandService;

        [HttpPost]
        public IActionResult Resize([FromBody]ResizeImageCommand command)
        {
            var response = _resizeImageCommandService.Execute(command);
            return response.ToActionResult();
        }
    }
}
