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
        public Stream ResponseStream { get; set; }
        public string ContentType { get; private set; }

        public CommandResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public CommandResponse NotFound()
        {
            return new CommandResponse(false)
            {
                IsNotFound = true
            };
        }

        public CommandResponse BadRequest()
        {
            return new CommandResponse(false)
            {
                IsBadRequest = true
            };
        }

        public CommandResponse Conflict()
        {
            return new CommandResponse(false)
            {
                IsConflict = true
            };
        }

        public CommandResponse Forbidden()
        {
            return new CommandResponse(false)
            {
                IsForbidden = true
            };
        }

        public CommandResponse Stream(Stream stream, string contentType)
        {
            return new CommandResponse(true)
            { 
                IsStream = true,
                ResponseStream = stream,
                ContentType = contentType // Todo: Set these up as static values
            };
        }
    }
}
