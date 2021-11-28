namespace EmailSender.WebApi
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.ResponseCompression;
    using Newtonsoft.Json.Converters;
    using Middleware;
    using Configuration;
    using Serilog;

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
            builder.UseMiddleware<DomainControl>();

            builder.UseResponseCompression();
            builder.UseRouting();
            builder.UseEndpoints(endpoints => endpoints.MapControllers());
            builder.SetupSwaggerUi(_environment);
        }
    }
}