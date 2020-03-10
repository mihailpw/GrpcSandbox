using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Services;

namespace Grpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Connector.ConnectorClient(channel);

            var call = client.Send(Metadata.Empty);

            ReadAll(call.ResponseStream);

            while (true)
            {
                var request = new Request();

                Console.WriteLine("type: ");
                request.Type = Console.ReadLine();

                Console.WriteLine("data: ");
                var data = Console.ReadLine()?.Split(',').Select(ByteString.CopyFromUtf8);
                if (data != null)
                {
                    request.Data.Add(data);
                }

                await call.RequestStream.WriteAsync(request);
            }

            // ReSharper disable once FunctionNeverReturns
        }

        private static async void ReadAll(IAsyncStreamReader<Response> responseResponseStream)
        {
            await foreach (var reply in responseResponseStream.ReadAllAsync())
            {
                Console.WriteLine(string.Join(",", reply.Data.Select(d => d.ToStringUtf8())));
            }
        }
    }
}
