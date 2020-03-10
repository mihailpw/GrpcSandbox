using System;
using System.Collections.Generic;

namespace Grpc.Server.Common
{
    internal class ClientsRepository : IClientsRepository
    {
        private readonly Dictionary<Guid, IClient> _clients = new Dictionary<Guid, IClient>();

        public bool TryGetClient(Guid clientId, out IClient client)
        {
            return _clients.TryGetValue(clientId, out client);
        }

        public void RegisterClient(Guid clientId, IClient client)
        {
            _clients.Add(clientId, client);
        }

        public void UnregisterClient(Guid clientId)
        {
            _clients.Remove(clientId);
        }
    }
}