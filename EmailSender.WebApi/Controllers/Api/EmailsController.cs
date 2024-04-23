using EmailSender.Backend.Application.Emails;
using EmailSender.Backend.Shared.Attributes;
using EmailSender.WebApi.Controllers.Mappers;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EmailSender.WebApi.Dto;

namespace EmailSender.WebApi.Controllers.Api;

/// <summary>
/// Endpoints for email capabilities.
/// </summary>
[ApiVersion("1.0")]
public class EmailsController : ApiBaseController
{
    ///<inheritdoc />
    public EmailsController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Allows to send an email.
    /// </summary>
    /// <param name="payLoad">Payload with message details.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns></returns>
    [HttpPost]
    [BillableEndpoint]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> SendEmail([FromBody] SendEmailDto payLoad, [FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(EmailMapper.MapToSendEmailRequest(payLoad));

    /// <summary>
    /// Allows to verify email address.
    /// </summary>
    /// <param name="payLoad">Payload with email details.</param>
    /// <param name="privateKey">Required private key.</param>
    /// <returns></returns>
    [HttpPost]
    [BillableEndpoint]
    [ProducesResponseType(typeof(VerifyEmailCommandResult), StatusCodes.Status200OK)]
    public async Task<VerifyEmailCommandResult> VerifyEmail([FromBody] VerifyEmailDto payLoad, [FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(EmailMapper.MapToVerifyEmailRequest(payLoad));
}