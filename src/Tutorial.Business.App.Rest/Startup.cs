using Gazel.Logging;
using Gazel.ServiceClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Gazel.Web;

namespace Tutorial.Business.App.Rest
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration) => this.configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddApiExplorer();

            services.AddGazelApiApplication(configuration,
                serviceClient: c => c.Routine(ServiceUrl.Localhost(5000)),
                restApi: c => c.Standard(),
                logging: c => c.Log4Net(Gazel.Logging.LogLevel.Info, l => l.DefaultConsoleAppenders())
            );
            services.AddSwaggerGen(config =>
            {
                config.CustomSchemaIds(x => x.FullName);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseGazel();
            app.UseCors();
            app.UseEndpoints(endpoints =>
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger/index.html");

                    return Task.CompletedTask;
                })
            );
        }
    }
}


