using System.Diagnostics.CodeAnalysis;
using Logger = EmailSender.Backend.Configuration.Logger;
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
            CreateHostBuilder(configuration)
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureKestrel(options => options.AddServerHeader = false)
                    .UseStartup<Startup>())
                .Build()
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

    private static IHostBuilder CreateHostBuilder(IConfigurationRoot configurationRoot)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder => builder.AddConfiguration(configurationRoot))
            .UseSerilog();
    }
}