using System;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Server.Common;
using Grpc.Services;
using Microsoft.Extensions.Logging;

namespace Grpc.Server.Services
{
    public class ConnectorService : Connector.ConnectorBase
    {
        private readonly ILogger<ConnectorService> _logger;
        private readonly IClientsRepository _clientsRepository;
        private readonly IRequestsHandler _requestsHandler;

        public ConnectorService(
            ILogger<ConnectorService> logger,
            IClientsRepository clientsRepository,
            IRequestsHandler requestsHandler)
        {
            _logger = logger;
            _clientsRepository = clientsRepository;
            _requestsHandler = requestsHandler;
        }

        public override async Task Send(
            IAsyncStreamReader<Request> requestStream,
            IServerStreamWriter<Response> responseStream,
            ServerCallContext context)
        {
            var clientId = Guid.NewGuid();
            try
            {
                var client = new ServerStreamWriterAdapter(responseStream);
                _clientsRepository.RegisterClient(clientId, client);
                _logger.LogInformation($"Connected {clientId}");

                await foreach (var request in requestStream.ReadAllAsync())
                {
                    await _requestsHandler.HandleAsync(clientId, request, context.CancellationToken);
                }
            }
            catch (IOException) when (context.CancellationToken.IsCancellationRequested)
            {
            }
            finally
            {
                _clientsRepository.UnregisterClient(clientId);
                _logger.LogInformation($"Disconnected {clientId}");
            }
        }
    }
}
