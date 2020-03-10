using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;

namespace Grpc.Server.Common
{
    public interface IDataProcessor
    {
        string Type { get; }

        Task ProcessAsync(Guid clientId, IReadOnlyList<ByteString> requestData, CancellationToken cancellationToken);
    }
}