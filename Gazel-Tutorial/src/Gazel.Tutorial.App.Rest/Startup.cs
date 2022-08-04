using Gazel.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventiv.Todo.App.Rest
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration) => this.configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGazelApiApplication(configuration,
                serviceClient: c => c.Routine("http://localhost:5000/service"),
                restApi: c => c.Standard(),
                logging: c => c.Log4Net(Gazel.Logging.LogLevel.Info, l => l.DefaultConsoleAppenders())
            );
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGazel();
            app.UseCors();
        }
    }
}