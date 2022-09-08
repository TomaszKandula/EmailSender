using System.Threading.Tasks;
using EmailSender.Backend.Cqrs.Handlers.Queries.History;
using EmailSender.Backend.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace EmailSender.WebApi.Controllers;

[ApiVersion("1.0")]
public class HistoryController : BaseController
{
    public HistoryController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [ProducesResponseType(typeof(GetSentHistoryQueryResult), StatusCodes.Status200OK)]
    public async Task<GetSentHistoryQueryResult> GetSentHistory([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetSentHistoryQuery());

    [HttpGet]
    [RequireAdministrator]
    [ProducesResponseType(typeof(GetRequestHistoryQueryResult), StatusCodes.Status200OK)]
    public async Task<GetRequestHistoryQueryResult> GetRequestHistory([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetRequestHistoryQuery());
}