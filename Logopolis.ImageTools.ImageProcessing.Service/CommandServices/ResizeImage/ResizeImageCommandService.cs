using Logopolis.ImageTools.ImageProcessing.Domain.Commands;
using Logopolis.ImageTools.ImageProcessing.Domain.Core;
using Logopolis.ImageTools.ImageProcessing.Domain.Graphics;
using Logopolis.ImageTools.ImageProcessing.Domain.ServiceInterfaces;

using System;

namespace Logopolis.ImageTools.ImageProcessing.Service.CommandServices.ResizeImage
{
    public class ResizeImageCommandService : IResizeImageCommandService
    {
        public CommandResponse Execute(ResizeImageCommand command)
        {
            var image = LogopolisImage.FromStream(command.Image);

            image.Resize(150, 150);
            
            return CommandResponse.Stream(
                image.ToStream, 
                image.ContentType, 
                $"{Guid.NewGuid()}.{image.FileExtension}");
        }
    }
}
