using System;
using Splitit.Infra.Providers;
using Splitit.Infra.Repositories;
using Splitit.Splitit.Repositories;
using Splitit.Splitit.Services;

namespace Splitit.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Register HttpClient for ImdbActorProvider
            services.AddHttpClient<ImdbActorProvider>();

            // Register repositories and services
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IActorProvider, ImdbActorProvider>();
            services.AddScoped<ActorService>();

            // Other service registrations
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

