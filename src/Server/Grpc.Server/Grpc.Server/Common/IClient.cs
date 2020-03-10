using System.Threading;
using System.Threading.Tasks;
using Grpc.Services;

namespace Grpc.Server.Common
{
    public interface IClient
    {
        Task SendAsync(Response response, CancellationToken cancellationToken);
    }
}