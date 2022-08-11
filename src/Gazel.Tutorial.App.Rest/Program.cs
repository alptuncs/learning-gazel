using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Gazel.Tutorial.App.Rest
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