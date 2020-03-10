using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Services;

namespace Grpc.Server.Common
{
    internal class RequestsHandler : IRequestsHandler
    {
        private readonly IEnumerable<IDataProcessor> _dataProcessors;

        public RequestsHandler(IEnumerable<IDataProcessor> dataProcessors)
        {
            _dataProcessors = dataProcessors;
        }

        public async Task HandleAsync(
            Guid clientId,
            Request request,
            CancellationToken cancellationToken)
        {
            var processors = _dataProcessors.Where(p => p.Type == request.Type);
            var processTasks = processors.Select(p => p.ProcessAsync(clientId, request.Data, cancellationToken));
            await Task.WhenAll(processTasks);
        }
    }
}