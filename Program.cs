using Microsoft.OpenApi.Models;
using Splitit.App.Middlewares;
using Splitit.Infra.Providers;
using Splitit.Infra.Repositories;
using Splitit.Splitit.Repositories;
using Splitit.Splitit.Services;

internal class Program
{
    //todo
    //1. db verification on data
    // 2. pagination
    // 3. response to crud
    // 4. error handlig
    // 5. swagger
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();
        Configure(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpClient();
        services.AddSingleton<IActorRepository, InMemoryActorRepository>();
        services.AddTransient<IActorProvider, ImdbActorProvider>();
        services.AddScoped<ActorService>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Splitit API", Version = "v1" });
        });
    }

    private static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthorization();
        app.MapControllers();
    }
}