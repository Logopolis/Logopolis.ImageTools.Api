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

        protected LogopolisImage(Image image)
        {
            _image = image;
        }

        protected void SetImage(Image image)
        {
            _image.Dispose();
            _image = image;
            _imageFormat = image.RawFormat;
        }

        public static LogopolisImage FromStream(Stream stream)
        {
            var image = Image.FromStream(stream);
            return new LogopolisImage(image);
        }

        public Stream ToStream()
        { 
            if(_bitmap == null)
            {
                return null;
            }

            using var outStream = new MemoryStream();
            _bitmap.Save(outStream, _imageFormat); 
            return outStream;
        }

        public void Dispose()
        {
            _image.Dispose();
        }
    }
}
