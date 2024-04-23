using EmailSender.Backend.Application.Smtp;
using EmailSender.Backend.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EmailSender.WebApi.Controllers.Api;

/// <summary>
/// 
/// </summary>
[ApiVersion("1.0")]
public class SmtpController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public SmtpController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    [HttpGet]
    [BillableEndpoint]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> GetServerStatus([FromQuery] string emailAddress, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetServerStatusQuery { EmailAddress = emailAddress });
}