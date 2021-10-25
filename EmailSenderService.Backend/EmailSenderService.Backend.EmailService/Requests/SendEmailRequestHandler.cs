namespace EmailSenderService.Backend.EmailService.Requests
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Services;
    using Shared.Resources;
    using Shared.Exceptions;

    public class SendEmailRequestHandler : TemplateHandler<SendEmailRequest, Unit>
    {
        private readonly ISenderService _senderService;

        public SendEmailRequestHandler(ISenderService senderService) => _senderService = senderService;
            
        public override async Task<Unit> Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            var isKeyExists = await _senderService.IsPrivateKeyExists(request.PrivateKey, cancellationToken);

            if (!isKeyExists)
                throw new BusinessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);

            var configuration = new Configuration
            {
                From = request.From,
                Subject = request.Subject,
                To = request.To,
                Cc = request.Cc,
                Bcc = request.Bcc,
                Body = request.Body,
                IsHtml = request.IsHtml
            };

            await _senderService.Send(configuration, cancellationToken);
            return Unit.Value;
        }
    }
}