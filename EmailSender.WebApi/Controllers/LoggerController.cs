namespace EmailSender.WebApi.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Backend.Cqrs.Handlers.Queries.Logger;
using MediatR;

[ApiVersion("1.0")]
public class LoggerController : BaseController
{
    public LoggerController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [ProducesResponseType(typeof(GetLogFilesListQueryResult), StatusCodes.Status200OK)]
    public async Task<GetLogFilesListQueryResult> GetLogFilesList([FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(new GetLogFilesListQuery());

    [HttpGet("{fileName}")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLogFileContent([FromRoute] string fileName, [FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(new GetLogFileContentQuery { LogFileName = fileName });
}