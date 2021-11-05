using Microsoft.AspNetCore.Mvc;

using Logopolis.ImageTools.ImageProcessing.Domain.Commands;
using Logopolis.ImageTools.ImageProcessing.Domain.ServiceInterfaces;
using Logopolis.ImageTools.Api.MvcExtensions;

using Microsoft.AspNetCore.Http;
using Logopolis.ImageTools.ImageProcessing.Domain.Core;

namespace Logopolis.ImageTools.Api.Controllers
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IResizeImageCommandService _resizeImageCommandService;

        public ImageController(IResizeImageCommandService resizeImageCommandService)
        {
            _resizeImageCommandService = resizeImageCommandService;
        }

        [HttpPost]
        [Route("image/resize")]
        public IActionResult Resize(
            [FromForm] int? maxHeight,
            [FromForm] int? maxWidth,
            [FromForm] int? absoluteHeight,
            [FromForm] int? absoluteWidth,
            [FromForm] bool? crop,
            [FromForm] IFormFile image)
        {
            if(image == null)
            {
                return CommandResponse
                    .BadRequest()
                    .WithMessage("Please supply image file")
                    .ToActionResult(this);
            }
            using var fileStream = image.OpenReadStream();

            var command = new ResizeImageCommand
            {
                MaxHeight = maxHeight,
                MaxWidth = maxWidth,
                AbsoluteHeight = absoluteHeight,
                AbsoluteWidth = absoluteWidth,
                Crop = crop,
                Image = fileStream
            };

            var response = _resizeImageCommandService.Execute(command);
            return response.ToActionResult(this);
        }

        [HttpPost]
        [Route("image/thumbnail")]
        public IActionResult Thumbnail(
            [FromForm] int height,
            [FromForm] int width,
            [FromForm] IFormFile image)
        {
            if (image == null)
            {
                return CommandResponse
                    .BadRequest()
                    .WithMessage("Please supply image file")
                    .ToActionResult(this);
            }
            using var fileStream = image.OpenReadStream();

            var command = new ResizeImageCommand
            {
                AbsoluteHeight = height,
                AbsoluteWidth = width,
                Crop = true,
                Image = fileStream
            };

            var response = _resizeImageCommandService.Execute(command);
            return response.ToActionResult(this);
        }
    }
}
