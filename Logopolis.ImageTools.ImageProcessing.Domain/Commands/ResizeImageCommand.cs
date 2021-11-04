using System.IO;

namespace Logopolis.ImageTools.ImageProcessing.Domain.Commands
{
    public class ResizeImageCommand
    {
        /// <summary>
        /// The maximum height of the resulting image.
        /// </summary>
        public int? MaxHeight { get; set; }

        /// <summary>
        /// The maximum width of the resulting image.
        /// </summary>
        public int? MaxWidth { get; set; }

        /// <summary>
        /// The required height of the resulting image. This will override max_height.
        /// If both this and absolute_width are provided, the resulting image will be cropped or stretched to fill.
        /// If only absolute_height is provided, the resulting width will be determined by the image ratio.
        /// </summary>
        public int? AbsoluteHeight { get; set; }

        /// <summary>
        /// The required width of the resulting image. This will override max_height.
        /// If both this and absolute_height are provided, the resulting image will be cropped or stretched to fill.
        /// If only absolute_width is provided, the resulting height will be determined by the image ratio.
        /// </summary>
        public int? AbsoluteWidth { get; set; }

        /// <summary>
        /// The image file to process
        /// </summary>
        public Stream Image { get; set; }

        /// <summary>
        /// Only when absolute_height and absolute_width are supplied.
        /// Defaults to false.
        /// </summary>
        public bool? Crop { get; set; } = false;
    }
}
