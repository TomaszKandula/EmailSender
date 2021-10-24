namespace EmailSenderService.WebApi.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Shared.Dto;
    using Backend.EmailService;
    using Backend.Shared.Attributes;
    using Backend.EmailService.Models;

    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService) => _emailService = emailService;

        [HttpPost]
        [ETagFilter(204)]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailDto payLoad)
        {
            var config = new Configuration
            {
                From = payLoad.From,
                Subject = payLoad.Subject,
                To = payLoad.To,
                Cc = payLoad.Cc,
                Bcc = payLoad.Bcc,
                Body = payLoad.Body,
                IsHtml = payLoad.IsHtml
            };

            await _emailService.Send(config, CancellationToken.None);
            return NoContent();
        }
    }
}