﻿using Microsoft.AspNetCore.Mvc;

using Logopolis.ImageTools.ImageProcessing.Domain.Commands;
using Logopolis.ImageTools.ImageProcessing.Domain.ServiceInterfaces;
using Logopolis.ImageTools.Api.MvcExtensions;

using Microsoft.AspNetCore.Http;

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
            [FromForm] IFormFile image)
        {
            using var fileStream = image.OpenReadStream();

            var command = new ResizeImageCommand
            {
                MaxHeight = maxHeight,
                MaxWidth = maxWidth,
                AbsoluteHeight = absoluteHeight,
                AbsoluteWidth = absoluteWidth,
                Image = fileStream
            };

            var response = _resizeImageCommandService.Execute(command);
            return response.ToActionResult(this);
        }
    }
}
