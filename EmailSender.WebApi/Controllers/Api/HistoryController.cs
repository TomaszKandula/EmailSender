using EmailSender.Backend.Application.History;
using EmailSender.Backend.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EmailSender.WebApi.Controllers.Api;

/// <summary>
/// 
/// </summary>
[ApiVersion("1.0")]
public class HistoryController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public HistoryController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetSentHistoryQueryResult), StatusCodes.Status200OK)]
    public async Task<GetSentHistoryQueryResult> GetSentHistory([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetSentHistoryQuery());

    /// <summary>
    /// 
    /// </summary>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpGet]
    [RequireAdministrator]
    [ProducesResponseType(typeof(GetRequestHistoryQueryResult), StatusCodes.Status200OK)]
    public async Task<GetRequestHistoryQueryResult> GetRequestHistory([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetRequestHistoryQuery());
}