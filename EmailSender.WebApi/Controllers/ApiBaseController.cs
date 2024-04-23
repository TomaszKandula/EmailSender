using EmailSender.Backend.Core.Errors;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EmailSender.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status500InternalServerError)]
public class ApiBaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected const string HeaderName = "X-Private-Key";

    public ApiBaseController(IMediator mediator) => Mediator = mediator;
}