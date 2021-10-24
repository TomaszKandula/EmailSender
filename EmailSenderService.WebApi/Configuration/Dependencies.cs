namespace EmailSenderService.WebApi.Configuration
{
    using System;
    using System.Net.Http;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Backend.Database;
    using Backend.SmtpService;
    using Backend.EmailService;
    using Backend.Shared.Models;
    using Backend.SmtpService.Models;
    using Backend.Database.Initializer;
    using Backend.Shared.Services.LoggerService;
    using Backend.Shared.Services.DateTimeService;
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
            SetupAppSettings(services, configuration);
            SetupLogger(services);
            SetupServices(services);
            SetupValidators(services);
        }
    
        private static void SetupAppSettings(IServiceCollection services, IConfiguration configuration) 
        {
            services.AddSingleton(configuration.GetSection(nameof(ApplicationPaths)).Get<ApplicationPaths>());
            services.AddSingleton(configuration.GetSection(nameof(SmtpServer)).Get<SmtpServer>());
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
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISmtpClientService, SmtpClientService>();
            services.AddScoped<IDbInitializer, DbInitializer>();
        }
    
        private static void SetupValidators(IServiceCollection services)
            => services.AddValidatorsFromAssemblyContaining<Startup>();

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