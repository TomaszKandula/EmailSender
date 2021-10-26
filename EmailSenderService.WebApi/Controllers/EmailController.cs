namespace EmailSenderService.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Shared.Dto;
    using Backend.EmailService.Mappers;
    using Backend.EmailService.Requests;
    using Backend.EmailService.Responses;
    using MediatR;

    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<Unit> SendEmail([FromBody] SendEmailDto payLoad)
            => await _mediator.Send(EmailMapper.MapToSendEmailRequest(payLoad));

        [HttpGet]
        public async Task<GetAllowDomainsResponse> GetAllowDomains([FromQuery] string key) 
            => await _mediator.Send(new GetAllowDomainsRequest { PrivateKey = key });

        [HttpGet]
        public async Task<GetAllowEmailsResponse> GetAllowEmails([FromQuery] string key) 
            => await _mediator.Send(new GetAllowEmailsRequest { PrivateKey = key });

        [HttpGet]
        public async Task<GetSentHistoryResponse> GetSentHistory([FromQuery] string key) 
            => await _mediator.Send(new GetSentHistoryRequest { PrivateKey = key });

        [HttpGet]
        public async Task<GetUserDetailsResponse> GetUserDetails([FromQuery] string key) 
            => await _mediator.Send(new GetUserDetailsRequest { PrivateKey = key });

        [HttpGet]
        public async Task<Unit> GetServerStatus([FromQuery] string key, string address) 
            => await _mediator.Send(new GetServerStatusRequest { PrivateKey = key, EmailAddress = address });
    }
}