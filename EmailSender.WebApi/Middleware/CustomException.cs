namespace EmailSender.WebApi.Middleware
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Http;
    using Backend.Shared.Resources;
    using Configuration;
    using Backend.Core.Models;
    using Backend.Core.Exceptions;

    [ExcludeFromCodeCoverage]
    public class CustomException
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomException(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate.Invoke(httpContext);
            }
            catch (ValidationException validationException)
            {
                var applicationError = new ApplicationError(validationException.ErrorCode, validationException.Message, validationException.ValidationResult);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (BusinessException businessException)
            {
                var applicationError = new ApplicationError(businessException.ErrorCode, businessException.Message);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (ProcessException processException)
            {
                var applicationError = new ApplicationError(processException.ErrorCode, processException.Message);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.UnprocessableEntity).ConfigureAwait(false);
            }
            catch (ServerException serverException)
            {
                var applicationError = new ApplicationError(serverException.ErrorCode, serverException.Message);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                var innerMessage = exception.InnerException?.Message; 
                var applicationError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), exception.Message, innerMessage);
                await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        private static Task WriteErrorResponse(HttpContext httpContext, ApplicationError applicationError, HttpStatusCode statusCode)
        {
            var result = JsonSerializer.Serialize(applicationError);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;
            CorsHeaders.Apply(httpContext);
            return httpContext.Response.WriteAsync(result);
        }
    }
}