using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Services;

namespace Grpc.Server.Common
{
    public interface IRequestsHandler
    {
        Task HandleAsync(
            Guid clientId,
            Request request,
            CancellationToken cancellationToken);
    }
}