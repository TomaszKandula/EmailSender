namespace EmailSender.WebApi;

using System;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Backend.Database.Initializer;
using Serilog.Events;
using Serilog;

[ExcludeFromCodeCoverage]
public static class Program
{
    private static readonly string EnvironmentValue 
        = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    private static readonly bool IsDevelopment 
        = EnvironmentValue == Environments.Development;

    private const string LogTemplate 
        = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static int Main(string[] args)
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    GetLogPathFile(),
                    outputTemplate: LogTemplate,
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: null,
                    shared: false
                ).CreateLogger();

            Log.Information("Starting WebHost...");
            Log.Information("Environment: {Environment}", EnvironmentValue);

            CreateWebHostBuilder(args)
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

    private static string GetLogPathFile()
    {
        var pathFolder = $"{AppDomain.CurrentDomain.BaseDirectory}logs";

        if (!Directory.Exists(pathFolder)) 
            Directory.CreateDirectory(pathFolder);

        return $"{pathFolder}{Path.DirectorySeparatorChar}log-.txt";
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseSerilog();
    }

    private static IWebHost MigrateDatabase(this IWebHost webHost)
    {
        var serviceScopeFactory = (IServiceScopeFactory) webHost.Services.GetService(typeof(IServiceScopeFactory));
        if (serviceScopeFactory == null) 
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