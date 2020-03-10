using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Services;

namespace Grpc.Server.Common.DataProcessors
{
    internal class SendToClientProcessor : IDataProcessor
    {
        private readonly IClientsRepository _clientsRepository;

        public SendToClientProcessor(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        public string Type => "sc";

        public async Task ProcessAsync(
            Guid clientId,
            IReadOnlyList<ByteString> requestData,
            CancellationToken cancellationToken)
        {
            var clientIdBytes = requestData.FirstOrDefault();
            if (clientIdBytes == null)
            {
                return;
            }

            if (!Guid.TryParse(clientIdBytes.ToStringUtf8(), out var clientIdGuid))
            {
                return;
            }

            if (!_clientsRepository.TryGetClient(clientIdGuid, out var client))
            {
                return;
            }

            var response = new Response
            {
                Type = $"{Type}-answer",
            };
            response.Data.Add(requestData.Skip(1));

            await client.SendAsync(response, cancellationToken);
        }
    }
}