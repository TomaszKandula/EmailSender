using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.WebApi.Configuration;
using EmailSender.WebApi.Middleware;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;

namespace EmailSender.WebApi;

[ExcludeFromCodeCoverage]
public class Startup
{
    private readonly IConfiguration _configuration;

    private readonly IHostEnvironment _environment;

    public Startup(IConfiguration configuration, IHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ErrorResponses = new ApiVersionException();
        });
        services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());
        services.RegisterDependencies(_configuration);
        services.SetupSwaggerOptions(_environment);
        services.SetupDockerInternalNetwork();
    }

    public void Configure(IApplicationBuilder builder)
    {
        builder.UseSerilogRequestLogging();

        builder.UseHttpsRedirection();
        builder.UseForwardedHeaders();
        builder.ApplyCorsPolicy();

        builder.UseMiddleware<Exceptions>();
        builder.UseMiddleware<CacheControl>();

        builder.UseResponseCompression();
        builder.UseRouting();
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context 
                => context.Response.WriteAsync("Email Sender API"));
        });

        builder.SetupSwaggerUi(_environment);
    }
}