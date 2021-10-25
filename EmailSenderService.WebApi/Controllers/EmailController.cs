namespace EmailSenderService.WebApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Shared.Dto;
    using Backend.Shared.Attributes;
    using Backend.EmailService.Mappers;
    using MediatR;

    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [ETagFilter(204)]
        public async Task<Unit> SendEmail([FromBody] SendEmailDto payLoad)
            => await _mediator.Send(EmailMapper.MapToSendEmailRequest(payLoad));
    }
}