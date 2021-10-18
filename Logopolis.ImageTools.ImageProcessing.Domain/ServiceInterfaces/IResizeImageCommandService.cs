using Logopolis.ImageTools.ImageProcessing.Domain.Commands;
using Logopolis.ImageTools.ImageProcessing.Domain.Core;

namespace Logopolis.ImageTools.ImageProcessing.Domain.ServiceInterfaces
{
    public interface IResizeImageCommandService
    {
        CommandResponse Execute(ResizeImageCommand command);
    }
}
