using EmailSender.Backend.Application.Mailcow;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.WebApi.Controllers.Api;

/// <summary>
/// Proxy to Mailcow server. 
/// </summary>
[ApiVersion("1.0")]
public class MailcowController : ApiBaseController
{
    ///<inheritdoc />
    public MailcowController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns Mailcow services status.
    /// </summary>
    /// <returns>Object w/details.</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetMailcowStatusQueryResult), StatusCodes.Status200OK)]
    public async Task<GetMailcowStatusQueryResult> GetStatus()
        => await Mediator.Send(new GetMailcowStatusQuery());
}