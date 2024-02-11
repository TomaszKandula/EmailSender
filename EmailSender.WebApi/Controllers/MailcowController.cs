using EmailSender.Backend.Application.Mailcow;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.WebApi.Controllers;

[ApiVersion("1.0")]
public class MailcowController : BaseController
{
    public MailcowController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [ProducesResponseType(typeof(GetMailcowStatusQueryResult), StatusCodes.Status200OK)]
    public async Task<GetMailcowStatusQueryResult> GetStatus()
        => await Mediator.Send(new GetMailcowStatusQuery());
}