using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MediatR;
using DnsClient;
using EmailSender.Backend.Configuration;
using EmailSender.Backend.Core.Utilities.DateTimeService;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Persistence.Database;
using EmailSender.Services.BehaviourService;
using EmailSender.Services.HttpClientService;
using EmailSender.Services.HttpClientService.Abstractions;
using EmailSender.Services.SenderService;
using EmailSender.Services.SmtpService;
using EmailSender.Services.UserService;
using MailKit.Net.Smtp;
using FluentValidation;

namespace EmailSender.WebApi;

/// <summary>
/// Register application dependencies.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Dependencies
{
    /// <summary>
    /// Register all services.
    /// </summary>
    /// <param name="services">Service collections.</param>
    /// <param name="configuration">Provided configuration.</param>
    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.CommonServices();
        SetupDatabase(services, configuration);
        PollySupport.SetupRetryPolicyWithPolly(services);
    }

    /// <summary>
    /// Register common services.
    /// </summary>
    /// <param name="services">Service collections.</param>
    public static void CommonServices(this IServiceCollection services)
    {
        SetupLogger(services);
        SetupServices(services);
        SetupValidators(services);
        SetupMediatR(services);
    }

    private static void SetupLogger(IServiceCollection services) 
        => services.AddSingleton<ILoggerService, LoggerService>();

    private static void SetupDatabase(IServiceCollection services, IConfiguration configuration) 
    {
        const int maxRetryCount = 10;
        var maxRetryDelay = TimeSpan.FromSeconds(5);

        services.AddDbContext<DatabaseContext>(options =>
        {
            var connectionString = configuration.GetValue<string>("DbConnect");
            options.UseSqlServer(connectionString, addOptions 
                => addOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null));
        });
    }

    private static void SetupServices(IServiceCollection services) 
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IHttpClientServiceFactory>(_ => new HttpClientServiceFactory());

        services.AddScoped<ISmtpClient, SmtpClient>();
        services.AddScoped<ILookupClient, LookupClient>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<ISenderService, SenderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISmtpClientService, SmtpClientService>();
    }

    private static void SetupValidators(IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining<Backend.Application.RequestHandler<IRequest, Unit>>();

    private static void SetupMediatR(IServiceCollection services) 
    {
        services.AddMediatR(options => options.AsScoped(), 
            typeof(Backend.Application.RequestHandler<IRequest, Unit>).GetTypeInfo().Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AddressCheckBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PrivateKeyCheckBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UserRoleCheckBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ApiRequestBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));
    }
}