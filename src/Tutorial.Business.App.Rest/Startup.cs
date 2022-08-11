using Gazel.RestApi;
using Gazel.ServiceClient;
using static System.Net.Http.HttpMethod;

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
                restApi: c => c.Standard(templateOptions: t =>
                {
                    t.Routes.Clear();
                    t.Routes.Add(
                        new(@"ICartInfo ICartService[.]GetCartWithName\(String name\)", (o, g) => new()
                        {
                            HttpMethod = Get,
                            Template = $"/carts/{{name}}",
                            Parameter = new("name", o.Parameters[0])
                        })
                    );
                    t.Routes.AddStandardRoutes();
                }),
                logging: c => c.Log4Net(Gazel.Logging.LogLevel.Debug, l => l.DefaultConsoleAppenders())
            );
            services.AddSwaggerGen(config =>
            {
                config.CustomSchemaIds(s => s.FullName.Replace('+', '.'));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
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


