using EmailSender.Backend.Application.Mailcow;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.WebApi.Controllers.Api;

/// <summary>
/// 
/// </summary>
[ApiVersion("1.0")]
public class MailcowController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public MailcowController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GetMailcowStatusQueryResult), StatusCodes.Status200OK)]
    public async Task<GetMailcowStatusQueryResult> GetStatus()
        => await Mediator.Send(new GetMailcowStatusQuery());
}