using EmailSender.Backend.Application.Emails;
using EmailSender.Backend.Shared.Attributes;
using EmailSender.WebApi.Controllers.Mappers;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EmailSender.WebApi.Dto;

namespace EmailSender.WebApi.Controllers;

[ApiVersion("1.0")]
public class EmailsController : BaseController
{
    public EmailsController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    [BillableEndpoint]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> SendEmail([FromBody] SendEmailDto payLoad, [FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(EmailMapper.MapToSendEmailRequest(payLoad));

    [HttpPost]
    [BillableEndpoint]
    [ProducesResponseType(typeof(VerifyEmailCommandResult), StatusCodes.Status200OK)]
    public async Task<VerifyEmailCommandResult> VerifyEmail([FromBody] VerifyEmailDto payLoad, [FromHeader(Name = HeaderName)] string privateKey)
        => await Mediator.Send(EmailMapper.MapToVerifyEmailRequest(payLoad));
}