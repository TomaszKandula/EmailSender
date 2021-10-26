namespace EmailSender.WebApi.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.OpenApi.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class Swagger
    {
        public static void SetupSwaggerOptions(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EmailSenderService API", 
                    Version = "v1"
                });
            });
        }

        public static void SetupSwaggerUi(IApplicationBuilder builder, IConfiguration configuration)
        {
            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "EmailSenderService API");
            });
        }
    }
}