using Gazel.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gazel.Tutorial.App.Service
{
    public class Startup
    {
        private readonly IConfiguration cfg;

        public Startup(IConfiguration cfg)
        {
            this.cfg = cfg;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGazelServiceApplication(cfg,
                database: c => c.Sqlite("gazel.tutorial.db"),
                service: c => c.Routine("http://localhost:5000/service"),
                logging: c => c.Log4Net(Logging.LogLevel.Info, l => l.DefaultConsoleAppenders()),
                authentication: c => c.AllowAnonymous(),
                authorization: c => c.AllowAll()
            );
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGazel();
        }
    }
}