using Azure.Identity;
using Serilog;
using System.Reflection;
using WebApi.KeyVault;

namespace WebApi;
public static class ConfigurationProvider
{

    public static void InitConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.Sources.Clear();
        builder.Configuration
                        .AddJsonFile("appsettings.json")
                        .AddUserSecrets(Assembly.GetEntryAssembly()!);

        if (builder.Environment.IsProduction())
        {
            builder.Configuration
                        .AddAzureKeyVault(new Uri("https://keyvault-ayoub-group1.vault.azure.net/"),
                                          new ManagedIdentityCredential(),
                                          new PrefixKeyVaultSecretManager("BookRental"))
                        .Build();
        }

    }

    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
        builder.Services.AddSingleton(Log.Logger);
        builder.Host.UseSerilog();
    }

}
