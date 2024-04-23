using EmailSender.Backend.Core.Errors;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EmailSender.WebApi.Controllers;

/// <summary>
/// Base controller with MediatR.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status500InternalServerError)]
public class ApiBaseController : ControllerBase
{
    /// <summary>
    /// Mediator instance.
    /// </summary>
    protected readonly IMediator Mediator;

    /// <summary>
    /// Expected header name.
    /// </summary>
    protected const string HeaderName = "X-Private-Key";

    /// <summary>
    /// Base controller.
    /// </summary>
    /// <param name="mediator"></param>
    public ApiBaseController(IMediator mediator) => Mediator = mediator;
}