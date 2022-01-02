namespace EmailSender.WebApi.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Backend.Cqrs.Handlers.Queries.Smtp;
using MediatR;

[ApiVersion("1.0")]
public class SmtpController : BaseController
{
    public SmtpController(IMediator mediator) : base(mediator) { }
    
    [HttpGet]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> GetServerStatus([FromQuery] string address, [FromHeader(Name = HeaderName)] string privateKey) 
        => await Mediator.Send(new GetServerStatusQuery { EmailAddress = address });
}