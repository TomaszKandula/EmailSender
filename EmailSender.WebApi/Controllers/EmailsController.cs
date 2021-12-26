namespace EmailSender.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Backend.Shared.Dto;
    using Backend.Cqrs.Mappers;
    using Backend.Cqrs.Handlers.Queries.Smtp;
    using Backend.Cqrs.Handlers.Queries.Users;
    using Backend.Cqrs.Handlers.Queries.Emails;
    using Backend.Cqrs.Handlers.Commands.Emails;
    using MediatR;

    [ApiVersion("1.0")]
    public class EmailsController : BaseController
    {
        public EmailsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<Unit> SendEmail([FromBody] SendEmailDto payLoad)
            => await Mediator.Send(EmailMapper.MapToSendEmailRequest(payLoad));

        [HttpPost]
        [ProducesResponseType(typeof(VerifyEmailCommandResult), StatusCodes.Status200OK)]
        public async Task<VerifyEmailCommandResult> VerifyEmail([FromBody] VerifyEmailDto payLoad)
            => await Mediator.Send(EmailMapper.MapToVerifyEmailRequest(payLoad));

        [HttpGet]
        [ProducesResponseType(typeof(GetAllowDomainsQueryResult), StatusCodes.Status200OK)]
        public async Task<GetAllowDomainsQueryResult> GetAllowDomains([FromQuery] string key) 
            => await Mediator.Send(new GetAllowDomainsQuery { PrivateKey = key });

        [HttpGet]
        [ProducesResponseType(typeof(GetAllowEmailsQueryResult), StatusCodes.Status200OK)]
        public async Task<GetAllowEmailsQueryResult> GetAllowEmails([FromQuery] string key) 
            => await Mediator.Send(new GetAllowEmailsQuery { PrivateKey = key });

        [HttpGet]
        [ProducesResponseType(typeof(GetEmailsHistoryQueryResult), StatusCodes.Status200OK)]
        public async Task<GetEmailsHistoryQueryResult> GetSentHistory([FromQuery] string key) 
            => await Mediator.Send(new GetEmailsHistoryQuery { PrivateKey = key });

        [HttpGet]
        [ProducesResponseType(typeof(GetUserDetailsQueryResult), StatusCodes.Status200OK)]
        public async Task<GetUserDetailsQueryResult> GetUserDetails([FromQuery] string key) 
            => await Mediator.Send(new GetUserDetailsQuery { PrivateKey = key });

        [HttpGet]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<Unit> GetServerStatus([FromQuery] string key, string address) 
            => await Mediator.Send(new GetServerStatusQuery { PrivateKey = key, EmailAddress = address });
    }
}