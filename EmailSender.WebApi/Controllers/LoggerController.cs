using EmailSender.Backend.Application.Logger;
using EmailSender.Backend.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EmailSender.WebApi.Controllers;

[ApiVersion("1.0")]
public class LoggerController : BaseController
{
    public LoggerController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [RequireAdministrator]
    [ProducesResponseType(typeof(GetLogFilesListQueryResult), StatusCodes.Status200OK)]
    public async Task<GetLogFilesListQueryResult> GetLogFilesList([FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(new GetLogFilesListQuery());

    [HttpGet("{fileName}")]
    [RequireAdministrator]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLogFileContent([FromRoute] string fileName, [FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(new GetLogFileContentQuery { LogFileName = fileName });
}