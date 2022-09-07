using System;
using System.Net.Http;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using DnsClient;
using EmailSender.Backend.Core.Services.DateTimeService;
using EmailSender.Backend.Core.Services.LoggerService;
using EmailSender.Persistence.Database;
using EmailSender.Persistence.Database.Initializer;
using EmailSender.Services.BehaviourService;
using EmailSender.Services.SenderService;
using EmailSender.Services.SmtpService;
using EmailSender.Services.UserService;
using MailKit.Net.Smtp;
using FluentValidation;

namespace EmailSender.WebApi.Configuration;

[ExcludeFromCodeCoverage]
public static class Dependencies
{
    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.CommonServices(configuration);
        SetupDatabase(services, configuration);
        SetupRetryPolicyWithPolly(services);
    }

    public static void CommonServices(this IServiceCollection services, IConfiguration configuration)
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
            options.UseSqlServer(configuration.GetConnectionString("DbConnect"), addOptions 
                => addOptions.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null));
        });
    }

    private static void SetupServices(IServiceCollection services) 
    {
        services.AddHttpContextAccessor();

        services.AddScoped<HttpClient>();
        services.AddScoped<ISmtpClient, SmtpClient>();
        services.AddScoped<ILookupClient, LookupClient>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<ISenderService, SenderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISmtpClientService, SmtpClientService>();
        services.AddScoped<IDbInitializer, DbInitializer>();
    }

    private static void SetupValidators(IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining<Backend.Cqrs.RequestHandler<IRequest, Unit>>();

    private static void SetupMediatR(IServiceCollection services) 
    {
        services.AddMediatR(options => options.AsScoped(), 
            typeof(Backend.Cqrs.RequestHandler<IRequest, Unit>).GetTypeInfo().Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AddressCheckBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PrivateKeyCheckBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UserRoleCheckBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ApiRequestBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));
    }

    private static void SetupRetryPolicyWithPolly(IServiceCollection services)
    {
        services.AddHttpClient("RetryHttpClient", options =>
        {
            options.DefaultRequestHeaders.Add("Accept", "application/json");
            options.Timeout = TimeSpan.FromMinutes(5);
            options.DefaultRequestHeaders.ConnectionClose = true;
        }).AddPolicyHandler(PollyPolicyHandler.SetupRetry());
    }
}