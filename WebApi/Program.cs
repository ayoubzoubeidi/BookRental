using Application;
using Application.Common;
using Domain.Entities;
using Infrastructure;
using Serilog;
using WebApi.Common;

namespace WebApi;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.Local.json", optional: true)
                        .AddEnvironmentVariables()
                        .Build();

        builder.Logging.ClearProviders();
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        builder.Services.AddSingleton(Log.Logger);
        builder.Host.UseSerilog();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();
        builder.Services.AddPersistence(configuration);
        builder.Services.AddApplication();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseSerilogRequestLogging();
        app.UseHealthChecks("/Health");
        app.UseHttpsRedirection();
        app.UseCustomExceptionHandler();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}