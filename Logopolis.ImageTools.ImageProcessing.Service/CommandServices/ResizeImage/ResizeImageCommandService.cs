using Logopolis.ImageTools.ImageProcessing.Domain.Commands;
using Logopolis.ImageTools.ImageProcessing.Domain.Core;
using Logopolis.ImageTools.ImageProcessing.Domain.Graphics;
using Logopolis.ImageTools.ImageProcessing.Domain.ServiceInterfaces;
using Logopolis.ImageTools.ImageProcessing.Domain.ExtensionMethods;

using System;
using System.Drawing;

namespace Logopolis.ImageTools.ImageProcessing.Service.CommandServices.ResizeImage
{
    public class ResizeImageCommandService : IResizeImageCommandService
    {
        public CommandResponse Execute(ResizeImageCommand command)
        {
            // Validate command:

            // must supply a width or height
            if(
                !command.AbsoluteHeight.HasNonZeroPositiveValue()
                && !command.AbsoluteWidth.HasNonZeroPositiveValue()
                && !command.MaxHeight.HasNonZeroPositiveValue()
                && !command.MaxWidth.HasNonZeroPositiveValue())
            {
                return CommandResponse
                    .BadRequest()
                    .WithMessage("Please supply a width / height parameter.");
            }

            // cannot supply absolute and max parameters
            if(
                (command.AbsoluteHeight.HasNonZeroPositiveValue() || command.AbsoluteWidth.HasNonZeroPositiveValue())
                && (command.MaxHeight.HasNonZeroPositiveValue() || command.MaxWidth.HasNonZeroPositiveValue()))
            {
                return CommandResponse
                    .BadRequest()
                    .WithMessage("Please supply either absolute or max parameters; not both.");
            }

            var crop = command.Crop.HasValue && command.Crop.Value;
            // cannot use crop without absoluteHeight and absoluteWidth
            if (
                crop
                && (!command.AbsoluteHeight.HasNonZeroPositiveValue() || !command.AbsoluteWidth.HasNonZeroPositiveValue()))
            {
                return CommandResponse
                    .BadRequest()
                    .WithMessage("Please supply both absoluteHeight and absoluteWidth when using crop=true.");
            }

            // Image required
            if(command.Image == null || command.Image.Length == 0)
            {
                return CommandResponse
                    .BadRequest()
                    .WithMessage("Please supply image data");
            }

            //LogopolisImage image;
            try
            {
                using var image = LogopolisImage.FromStream(command.Image);

                // IF CROPPING HANDLE SEPARATELY
                if(crop)
                {
                    return ResizeWithCrop(command.AbsoluteHeight.Value, command.AbsoluteWidth.Value, image);
                }

                // ABSOLUTE CASES
                if(command.AbsoluteHeight.HasNonZeroPositiveValue() && !command.AbsoluteWidth.HasNonZeroPositiveValue())
                {
                    image.Resize(
                        command.AbsoluteHeight.Value,
                        (int)(command.AbsoluteHeight * image.Ratio));
                }
                else if(command.AbsoluteHeight.HasNonZeroPositiveValue() && command.AbsoluteWidth.HasNonZeroPositiveValue())
                {
                    image.Resize(
                        command.AbsoluteHeight.Value,
                        command.AbsoluteWidth.Value);
                }
                else if(command.AbsoluteWidth.HasNonZeroPositiveValue()) // command.AbsoluteHeight is not supplied
                {
                    image.Resize(
                        (int)(command.AbsoluteWidth / image.Ratio),
                        command.AbsoluteWidth.Value);
                }

                // MAX CASES
                else if(command.MaxHeight.HasNonZeroPositiveValue() && command.MaxWidth.HasNonZeroPositiveValue())
                {
                    if(command.MaxWidth > (command.MaxHeight * image.Ratio))
                    {
                        // the max height is what constrains the image
                        image.Resize(
                            command.MaxHeight.Value,
                            (int)(command.MaxHeight.Value * image.Ratio));
                    }
                    else
                    {
                        // the max width is what constrains the image
                        image.Resize(
                            (int)(command.MaxWidth.Value / image.Ratio),
                            command.MaxWidth.Value);
                    }
                }
                else if(command.MaxWidth.HasNonZeroPositiveValue()) // command.MaxHeight is not supplied
                {
                    image.Resize(
                        (int)(command.MaxWidth.Value / image.Ratio),
                        command.MaxWidth.Value);
                }
                else if(command.MaxHeight.HasNonZeroPositiveValue()) // command.MaxWidth is not supplied
                {
                    image.Resize(
                        command.MaxHeight.Value,
                        (int)(command.MaxHeight.Value * image.Ratio));
                }

                return CommandResponse.Stream(
                    image.ToStream(), 
                    image.ContentType, 
                    $"{Guid.NewGuid()}.{image.FileExtension}");
            }
            catch
            {
                return CommandResponse
                    .BadRequest()
                    .WithMessage("Please supply valid image data in png, jpeg, gif or bmp format.");
            }
        }

        private CommandResponse ResizeWithCrop(int height, int width, LogopolisImage image)
        {
            var outputRatio = (decimal)width / (decimal)height;

            if(outputRatio == image.Ratio)
            {
                image.Resize(height, width);
            }
            else if(image.Ratio > outputRatio)
            {
                // the output needs cropping horizontally
                var resizeRatio = (decimal)height / (decimal)image.Height;
                var resizedWidth = (int)(resizeRatio * image.Width);
                image.Resize(height, resizedWidth);
                image.Crop(new Rectangle(resizedWidth / 2 - width / 2, 0, width, height));
            }
            else
            {
                // the output needs cropping horizontally
                var resizeRatio = (decimal)width / (decimal)image.Width;
                var resizedHeight = (int)(resizeRatio * image.Height);
                image.Resize(resizedHeight, width);
                image.Crop(new Rectangle(0, resizedHeight / 2 - height / 2, width, height));
            }

            return CommandResponse.Stream(
                image.ToStream(),
                image.ContentType,
                $"{Guid.NewGuid()}.{image.FileExtension}");
        }
    }
}
