using System.Diagnostics.CodeAnalysis;
using EmailSender.Persistence.Database.Initializer;
using Microsoft.AspNetCore;
using Logger = EmailSender.WebApi.Configuration.Logger;
using Serilog;

namespace EmailSender.WebApi;

/// <summary>
/// Program.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Program
{
    private static readonly string? EnvironmentValue 
        = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    private static readonly bool IsDevelopment 
        = EnvironmentValue == Environments.Development;

    /// <summary>
    /// Main entry point.
    /// </summary>
    /// <returns></returns>
    public static int Main()
    {
        try
        {
            var configuration = GetConfiguration();
            const string fileName = @"logs/EmailSender.WebApi/{yyyy}{MM}{dd}.txt";
            Log.Logger = Logger.Configuration.GetLogger(configuration, fileName);
            Log.Information("Starting WebHost...");
            Log.Information("Environment: {Environment}", EnvironmentValue);
            CreateWebHostBuilder(configuration)
                .Build()
                .MigrateDatabase()
                .Run();

            return 0;
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "WebHost has been terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IConfigurationRoot GetConfiguration()
    {
        var appSettingsEnv = $"appsettings.{EnvironmentValue}.json";
        return new ConfigurationBuilder()
            .AddJsonFile(appSettingsEnv, true, true)
            .AddUserSecrets<Startup>(true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static IWebHostBuilder CreateWebHostBuilder(IConfigurationRoot configurationRoot)
    {
        return WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder => builder.AddConfiguration(configurationRoot))
            .ConfigureKestrel(options => options.AddServerHeader = false)
            .UseStartup<Startup>()
            .UseSerilog();
    }

    private static IWebHost MigrateDatabase(this IWebHost webHost)
    {
        if (webHost.Services.GetService(typeof(IServiceScopeFactory)) is not IServiceScopeFactory serviceScopeFactory) 
            return webHost;
            
        using var scope = serviceScopeFactory.CreateScope();
        var services = scope.ServiceProvider;
        var dbInitializer = services.GetRequiredService<IDbInitializer>();

        if (!IsDevelopment) 
            return webHost;

        dbInitializer.StartMigration();
        dbInitializer.SeedData();

        return webHost;
    }
}