using System.Net;
using System.Diagnostics.CodeAnalysis;
using EmailSender.Backend.Core.Errors;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Shared.Resources;
using Newtonsoft.Json;

namespace EmailSender.WebApi.Middleware;

/// <summary>
/// Exceptions middleware.
/// </summary>
[ExcludeFromCodeCoverage]
public class Exceptions
{
    private readonly RequestDelegate _requestDelegate;

    /// <summary>
    /// Exceptions middleware.
    /// </summary>
    /// <param name="requestDelegate">Delegate instance.</param>
    public Exceptions(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

    /// <summary>
    /// Pre-define application exceptions for status codes:
    /// <list>
    ///   <item>400 - Bad Request</item>
    ///   <item>401 - Unauthorized</item>
    ///   <item>403 - Forbidden</item>
    ///   <item>422 - Unprocessable Entity</item>
    ///   <item>500 - Internal Server Error</item>
    /// </list>
    /// </summary>
    /// <param name="httpContext">Current HTTP context.</param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch (ValidationException validationException)
        {
            var applicationError = new ApplicationError(validationException.ErrorCode, validationException.Message, validationException.ValidationResult);
            await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
        }
        catch (AuthorizationException authenticationException)
        {
            var innerMessage = authenticationException.InnerException?.Message ?? "";
            var applicationError = new ApplicationError(authenticationException.ErrorCode, authenticationException.Message, innerMessage);
            await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.Unauthorized).ConfigureAwait(false);
        }
        catch (AccessException authorizationException)
        {
            var innerMessage = authorizationException.InnerException?.Message ?? "";
            var applicationError = new ApplicationError(authorizationException.ErrorCode, authorizationException.Message, innerMessage);
            await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.Forbidden).ConfigureAwait(false);
        }
        catch (BusinessException businessException)
        {
            var innerMessage = businessException.InnerException?.Message ?? "";
            var applicationError = new ApplicationError(businessException.ErrorCode, businessException.Message, innerMessage);
            await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.UnprocessableEntity).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            var innerMessage = exception.InnerException?.Message ?? "";
            var applicationError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), exception.Message, innerMessage);
            await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.InternalServerError).ConfigureAwait(false);
        }
    }

    private static Task WriteErrorResponse(HttpContext httpContext, ApplicationError applicationError, HttpStatusCode statusCode)
    {
        var result = JsonConvert.SerializeObject(applicationError);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;
        return httpContext.Response.WriteAsync(result);
    }
}