using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Logopolis.ImageTools.ImageProcessing.Domain.Graphics
{
    public class LogopolisImage : IDisposable
    {
        private Image _image;
        private Bitmap _bitmap;
        private ImageFormat _imageFormat;

        public int Width => _image.Width;
        public int Height => _image.Height;
        public double Ratio => _image.Width / Height;

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
            _bitmap?.Dispose();
            _bitmap = new Bitmap(_image, width, height);
            SetImage(_bitmap);
        }

        public Stream ToStream()
        { 
            if(_bitmap == null)
            {
                return null;
            }

            var outStream = new MemoryStream();
            _bitmap.Save(outStream, _imageFormat); 
            return outStream;
        }

        public void Dispose()
        {
            //_image.Dispose();
            //_bitmap.Dispose();
        }
    }
}
