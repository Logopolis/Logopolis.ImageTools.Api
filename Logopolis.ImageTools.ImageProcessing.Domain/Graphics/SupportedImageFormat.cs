using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;

namespace Logopolis.ImageTools.ImageProcessing.Domain.Graphics
{
    public class SupportedImageFormat
    {
        private static IEnumerable<SupportedImageFormat> _allFormats = new[]
        {
            Png(),
            Bmp(),
            Jpg(),
            Jpeg(),
            Tif()
        };

        public string FileExtension { get; }
        public ImageFormat ImageFormat { get; }
        public string ContentType { get; }

        public SupportedImageFormat(string fileExtension, ImageFormat imageFormat, string contentType)
        {
            FileExtension = fileExtension;
            ImageFormat = imageFormat;
            ContentType = contentType;
        }

        public static SupportedImageFormat Png()
            => new SupportedImageFormat("png", ImageFormat.Png, "image/png");

        public static SupportedImageFormat Jpg()
            => new SupportedImageFormat("jpg", ImageFormat.Jpeg, "image/jpeg");

        public static SupportedImageFormat Jpeg() => Jpg();

        public static SupportedImageFormat Bmp()
            => new SupportedImageFormat("bmp", ImageFormat.Bmp, "image/bmp");

        public static SupportedImageFormat Tif()
            => new SupportedImageFormat("tif", ImageFormat.Bmp, "image/tiff");

        public static SupportedImageFormat GetByFileExtension(string extension) => _allFormats
            .FirstOrDefault(f => f.FileExtension == extension);

        public static SupportedImageFormat GetByImageFormat(ImageFormat imageFormat) => _allFormats
            .FirstOrDefault(f => f.ImageFormat == imageFormat);
    }
}
