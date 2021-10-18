using Logopolis.ImageTools.ImageProcessing.Domain.Core;

using Microsoft.AspNetCore.Mvc;

namespace Logopolis.ImageTools.Api.MvcExtensions
{
    public static class CommandResponseExtensions
    {
        public static IActionResult ToActionResult(this CommandResponse response)
        {
            if (response.IsStream)
            {
                return new FileStreamResult(response.ResponseStream, response.ContentType);
            }


            if (response.IsNotFound)
            {
                return new NotFoundObjectResult(response.Message);
            }

            if (response.IsForbidden)
            {
                return new StatusCodeResult(403);
            }

            if (response.IsConflict)
            {
                return new ConflictObjectResult(response.Message);
            }

            if (response.IsBadRequest)
            {
                return new BadRequestObjectResult(response.Message);
            }

            return new OkObjectResult(response.ResponseBody);
        }
    }
}
