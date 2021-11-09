namespace EmailSender.WebApi.Configuration
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Backend.Database;
    using Backend.SmtpService;
    using Backend.EmailService;
    using Backend.Shared.Models;
    using Backend.Shared.Behaviours;
    using Backend.Database.Initializer;
    using Backend.Shared.Services.LoggerService;
    using Backend.Shared.Services.DateTimeService;
    using Backend.EmailService.Services.VatService;
    using Backend.EmailService.Services.UserService;
    using Backend.EmailService.Services.SenderService;
    using Backend.EmailService.Services.BillingService;
    using MediatR;
    using DnsClient;
    using MailKit.Net.Smtp;
    using FluentValidation;

    [ExcludeFromCodeCoverage]
    public static class Dependencies
    {
        public static void Register(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment = default)
        {
            CommonServices(services, configuration);
            SetupDatabase(services, configuration);
            if (environment != null)
                SetupRetryPolicyWithPolly(services, configuration, environment);
        }

        public static void CommonServices(IServiceCollection services, IConfiguration configuration)
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
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVatService, VatService>();
            services.AddScoped<ISmtpClientService, SmtpClientService>();
            services.AddScoped<IDbInitializer, DbInitializer>();
        }

        private static void SetupValidators(IServiceCollection services)
            => services.AddValidatorsFromAssemblyContaining<TemplateHandler<IRequest, Unit>>();

        private static void SetupMediatR(IServiceCollection services) 
        {
            services.AddMediatR(options => options.AsScoped(), 
                typeof(TemplateHandler<IRequest, Unit>).GetTypeInfo().Assembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
        }

        private static void SetupRetryPolicyWithPolly(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            var applicationPaths = configuration.GetSection(nameof(ApplicationPaths)).Get<ApplicationPaths>();
            services.AddHttpClient("RetryHttpClient", options =>
            {
                options.BaseAddress = new Uri(environment.IsDevelopment() 
                    ? applicationPaths.DevelopmentOrigin 
                    : applicationPaths.DeploymentOrigin);
                options.DefaultRequestHeaders.Add("Accept", "application/json");
                options.Timeout = TimeSpan.FromMinutes(5);
                options.DefaultRequestHeaders.ConnectionClose = true;
            }).AddPolicyHandler(Handlers.RetryPolicyHandler());
        }
    }
}