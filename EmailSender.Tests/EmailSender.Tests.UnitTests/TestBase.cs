using EmailSender.Backend.Core.Utilities.DataUtilityService;
using EmailSender.Backend.Core.Utilities.DateTimeService;
using EmailSender.Persistence.Database;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSender.Tests.UnitTests;

public class TestBase
{
    protected IDataUtilityService DataUtilityService { get; }

    protected IDateTimeService DateTimeService { get; }
        
    private readonly DatabaseContextFactory _databaseContextFactory;
        
    protected TestBase()
    {
        var services = new ServiceCollection();
        services.AddSingleton<DatabaseContextFactory>();
        services.AddScoped<IDataUtilityService, DataUtilityService>();
        services.AddScoped<IDateTimeService, DateTimeService>();

        using var serviceScope = services.BuildServiceProvider(true).CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        _databaseContextFactory = serviceProvider.GetService<DatabaseContextFactory>();
        DataUtilityService = serviceProvider.GetRequiredService<IDataUtilityService>();
        DateTimeService = serviceProvider.GetRequiredService<IDateTimeService>();
    }

    protected DatabaseContext GetTestDatabaseContext() =>  _databaseContextFactory.CreateDatabaseContext();
}