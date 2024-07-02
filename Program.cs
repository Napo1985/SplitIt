using Splitit.Infra.Providers;
using Splitit.Infra.Repositories;
using Splitit.Splitit.Repositories;
using Splitit.Splitit.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        //// Register your services here
        //builder.Services.AddHttpClient<ImdbActorProvider>();
        //builder.Services.AddScoped<IActorRepository, ActorRepository>();
        //builder.Services.AddScoped<IActorProvider, ImdbActorProvider>();
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<IActorRepository, ActorRepository>();
        builder.Services.AddTransient<IActorProvider, ImdbActorProvider>();
        builder.Services.AddScoped<ActorService>();



        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}