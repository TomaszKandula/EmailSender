using EmailSender.Backend.Application.History;
using EmailSender.Backend.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EmailSender.WebApi.Controllers.Api;

/// <summary>
/// Endpoints for history capabilities.
/// </summary>
[ApiVersion("1.0")]
public class HistoryController : ApiBaseController
{
    ///<inheritdoc />
    public HistoryController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns sent email history.
    /// </summary>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Object w/details.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetSentHistoryQueryResult), StatusCodes.Status200OK)]
    public async Task<GetSentHistoryQueryResult> GetSentHistory([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetSentHistoryQuery());

    /// <summary>
    /// Returns history of incoming requests.
    /// </summary>
    /// <param name="privateKey">Required private key.</param>
    /// <returns>Object w/details.</returns>
    [HttpGet]
    [RequireAdministrator]
    [ProducesResponseType(typeof(GetRequestHistoryQueryResult), StatusCodes.Status200OK)]
    public async Task<GetRequestHistoryQueryResult> GetRequestHistory([FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetRequestHistoryQuery());
}