using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Server.Common;
using Grpc.Services;

namespace Grpc.Server.Services
{
    internal class ServerStreamWriterAdapter : IClient
    {
        private readonly IServerStreamWriter<Response> _serverStreamWriter;

        public ServerStreamWriterAdapter(IServerStreamWriter<Response> serverStreamWriter)
        {
            _serverStreamWriter = serverStreamWriter;
        }

        public async Task SendAsync(Response response, CancellationToken cancellationToken)
        {
            await _serverStreamWriter.WriteAsync(response);
        }
    }
}