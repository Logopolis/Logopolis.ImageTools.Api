using System;
using System.IO;

namespace Logopolis.ImageTools.ImageProcessing.Domain.Core
{
    public class CommandResponse
    {
        public bool IsSuccess { get; }
        public bool IsNotFound { get; private set; }
        public bool IsForbidden { get; private set; }
        public bool IsBadRequest { get; private set; }
        public bool IsConflict { get; private set; }
        public bool IsStream { get; private set; }

        public string Message { get; set; }

        public dynamic ResponseBody { get; set; }
        public Func<Stream> GetResponseStream { get; set; }
        public string ContentType { get; private set; }
        public string FileName { get; private set; }

        public CommandResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static CommandResponse NotFound()
        {
            return new CommandResponse(false)
            {
                IsNotFound = true
            };
        }

        public static CommandResponse BadRequest()
        {
            return new CommandResponse(false)
            {
                IsBadRequest = true
            };
        }

        public static CommandResponse Conflict()
        {
            return new CommandResponse(false)
            {
                IsConflict = true
            };
        }

        public static CommandResponse Forbidden()
        {
            return new CommandResponse(false)
            {
                IsForbidden = true
            };
        }

        public static CommandResponse Stream(
            Func<Stream> getStream,
            string contentType,
            string fileName)
        {
            return new CommandResponse(true)
            { 
                IsStream = true,
                GetResponseStream = getStream,
                ContentType = contentType,
                FileName = fileName
            };
        }

        public CommandResponse WithMessage(string message)
        {
            Message = message;
            return this;
        }
    }
}
