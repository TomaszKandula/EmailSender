namespace EmailSender.WebApi.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.OpenApi.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class Swagger
    {
        private const string ApiVersion = "v1";

        private const string ApiName = "Email Sender API";

        public static void SetupSwaggerOptions(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = ApiName, 
                    Version = ApiVersion
                });
            });
        }

        public static void SetupSwaggerUi(this IApplicationBuilder builder)
        {
            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApiName);
            });
        }
    }
}