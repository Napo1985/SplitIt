using System;
using Splitit.Infra.Providers;

namespace Splitit.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register HttpClient
            services.AddHttpClient<ImdbActorProvider>();

            // Register ImdbActorProvider
            services.AddScoped<IActorProvider, ImdbActorProvider>();

            // Other service registrations
            // ...

            // Register controllers
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure middleware, routing, etc.
            // ...

            // Use endpoints for MVC
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

