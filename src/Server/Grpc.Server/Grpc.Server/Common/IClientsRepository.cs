using System;

namespace Grpc.Server.Common
{
    public interface IClientsRepository
    {
        bool TryGetClient(Guid clientId, out IClient client);
        void RegisterClient(Guid clientId, IClient client);
        void UnregisterClient(Guid clientId);
    }
}