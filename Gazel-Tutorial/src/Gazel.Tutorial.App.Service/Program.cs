using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Gazel.Tutorial.App.Service
{
    public class Program
    {
        public static void Main(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseGazelServiceProvider()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .Build()
                .Run();
    }
}