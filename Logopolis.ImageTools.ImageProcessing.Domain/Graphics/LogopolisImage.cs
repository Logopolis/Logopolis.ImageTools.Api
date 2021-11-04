using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Logopolis.ImageTools.ImageProcessing.Domain.Graphics
{
    public class LogopolisImage : IDisposable
    {
        private Image _image;
        private ImageFormat _imageFormat;

        public int Width => _image.Width;
        public int Height => _image.Height;
        
        /// <summary>
        /// Width divided by height e.g. 4 by 3 = 1.33
        /// </summary>
        public decimal Ratio => (decimal)_image.Width / (decimal)Height;

        public string ContentType =>
            SupportedImageFormat.GetByImageFormat(_imageFormat)
                .ContentType;

        public string FileExtension =>
            SupportedImageFormat.GetByImageFormat(_imageFormat)
                .FileExtension;

        protected LogopolisImage(Image image)
        {
            _image = image;
        }

        protected void SetImage(Image image)
        {
            _image.Dispose();
            _image = image;
        }

        public static LogopolisImage FromStream(Stream stream)
        {
            var image = Image.FromStream(stream);
            return new LogopolisImage(image)
            {
                _imageFormat = image.RawFormat
            };
        }

        public void Resize(int height, int width)
        {
            var bitmap = new Bitmap(_image, width, height);
            SetImage(bitmap);
        }

        public void Crop(Rectangle cropArea)
        {
            var bitmap = new Bitmap(_image);
            SetImage(bitmap.Clone(cropArea, bitmap.PixelFormat));
        }

        public Stream ToStream()
        { 
            if(_image == null)
            {
                return null;
            }

            var outStream = new MemoryStream();
            _image.Save(outStream, _imageFormat); 
            return outStream;
        }

        public void Dispose()
        {
            _image.Dispose();
        }
    }
}
