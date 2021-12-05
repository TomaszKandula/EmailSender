namespace EmailSender.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Backend.Shared.Dto;
    using Backend.Cqrs.Mappers;
    using Backend.Cqrs.Requests;
    using Backend.Cqrs.Responses;
    using MediatR;

    [ApiVersion("1.0")]
    public class EmailController : BaseController
    {
        public EmailController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<Unit> SendEmail([FromBody] SendEmailDto payLoad)
            => await Mediator.Send(EmailMapper.MapToSendEmailRequest(payLoad));

        [HttpPost]
        [ProducesResponseType(typeof(VerifyEmailCommandResponse), StatusCodes.Status200OK)]
        public async Task<VerifyEmailCommandResponse> VerifyEmail([FromBody] VerifyEmailDto payLoad)
            => await Mediator.Send(EmailMapper.MapToVerifyEmailRequest(payLoad));

        [HttpGet]
        [ProducesResponseType(typeof(GetAllowDomainsQueryResponse), StatusCodes.Status200OK)]
        public async Task<GetAllowDomainsQueryResponse> GetAllowDomains([FromQuery] string key) 
            => await Mediator.Send(new GetAllowDomainsQueryRequest { PrivateKey = key });

        [HttpGet]
        [ProducesResponseType(typeof(GetAllowEmailsQueryResponse), StatusCodes.Status200OK)]
        public async Task<GetAllowEmailsQueryResponse> GetAllowEmails([FromQuery] string key) 
            => await Mediator.Send(new GetAllowEmailsQueryRequest { PrivateKey = key });

        [HttpGet]
        [ProducesResponseType(typeof(GetSentHistoryQueryResponse), StatusCodes.Status200OK)]
        public async Task<GetSentHistoryQueryResponse> GetSentHistory([FromQuery] string key) 
            => await Mediator.Send(new GetSentHistoryQueryRequest { PrivateKey = key });

        [HttpGet]
        [ProducesResponseType(typeof(GetUserDetailsQueryResponse), StatusCodes.Status200OK)]
        public async Task<GetUserDetailsQueryResponse> GetUserDetails([FromQuery] string key) 
            => await Mediator.Send(new GetUserDetailsQueryRequest { PrivateKey = key });

        [HttpGet]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<Unit> GetServerStatus([FromQuery] string key, string address) 
            => await Mediator.Send(new GetServerStatusQueryRequest { PrivateKey = key, EmailAddress = address });
    }
}