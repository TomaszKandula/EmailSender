namespace EmailSender.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Backend.Shared.Dto;
    using Backend.EmailService.Mappers;
    using Backend.EmailService.Requests;
    using Backend.EmailService.Responses;
    using MediatR;

    public class EmailController : BaseController
    {
        public EmailController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<Unit> SendEmail([FromBody] SendEmailDto payLoad)
            => await Mediator.Send(EmailMapper.MapToSendEmailRequest(payLoad));

        [HttpPost]
        public async Task<VerifyEmailResponse> VerifyEmail([FromBody] VerifyEmailDto payLoad)
            => await Mediator.Send(EmailMapper.MapToVerifyEmailRequest(payLoad));

        [HttpGet]
        public async Task<GetAllowDomainsResponse> GetAllowDomains([FromQuery] string key) 
            => await Mediator.Send(new GetAllowDomainsRequest { PrivateKey = key });

        [HttpGet]
        public async Task<GetAllowEmailsResponse> GetAllowEmails([FromQuery] string key) 
            => await Mediator.Send(new GetAllowEmailsRequest { PrivateKey = key });

        [HttpGet]
        public async Task<GetSentHistoryResponse> GetSentHistory([FromQuery] string key) 
            => await Mediator.Send(new GetSentHistoryRequest { PrivateKey = key });

        [HttpGet]
        public async Task<GetUserDetailsResponse> GetUserDetails([FromQuery] string key) 
            => await Mediator.Send(new GetUserDetailsRequest { PrivateKey = key });

        [HttpGet]
        public async Task<Unit> GetServerStatus([FromQuery] string key, string address) 
            => await Mediator.Send(new GetServerStatusRequest { PrivateKey = key, EmailAddress = address });
    }
}