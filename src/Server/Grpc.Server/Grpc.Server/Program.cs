using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Grpc.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(b => b.UseStartup<Startup>())
               .Build()
               .Run();
        }
    }
}
