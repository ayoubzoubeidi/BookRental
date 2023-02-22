using Application;
using Infrastructure;
using Serilog;
using WebApi.Common;

namespace WebApi;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.InitConfiguration();
        builder.ConfigureSerilog();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseCustomExceptionHandler();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}