using EmailSender.Backend.Application.Smtp;
using EmailSender.Backend.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace EmailSender.WebApi.Controllers.Api;

/// <summary>
/// Endpoints for under-laying SMTP server. 
/// </summary>
[ApiVersion("1.0")]
public class SmtpController : ApiBaseController
{
    ///<inheritdoc />
    public SmtpController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns information whether SMTP can send emails from given email address.
    /// </summary>
    /// <param name="emailAddress">Email address to be checked.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns></returns>
    [HttpGet]
    [BillableEndpoint]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> GetServerStatus([FromQuery] string emailAddress, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetServerStatusQuery { EmailAddress = emailAddress });
}