namespace EmailSender.Backend.EmailService.Requests
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Services;
    using Shared.Resources;
    using Shared.Exceptions;

    public class GetServerStatusRequestHandler : TemplateHandler<GetServerStatusRequest, Unit>
    {
        private readonly ISenderService _senderService;

        public GetServerStatusRequestHandler(ISenderService senderService) => _senderService = senderService;

        public override async Task<Unit> Handle(GetServerStatusRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);
            var emailId = await _senderService.VerifyEmailFrom(request.EmailAddress, userId, cancellationToken);

            VerifyArguments(isKeyValid, userId, emailId);

            var result = await _senderService.VerifyConnection(emailId, cancellationToken);
            return result == null 
                ? Unit.Value 
                : throw new ServerException(nameof(ErrorCodes.SMTP_FAILED),$"SMTP service verification failed. Reason: {result.ErrorDesc}");
        }

        private static void VerifyArguments(bool isKeyValid, Guid? userId, Guid? emailId)
        {
            if (!isKeyValid)
            {
                var message = $"Cannot verify SMTP service. Reason: {ErrorCodes.INVALID_PRIVATE_KEY}";
                throw new ServerException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), message);
            }

            if (userId == null || userId == Guid.Empty)
            {
                var message = $"Cannot verify SMTP service. Reason: {ErrorCodes.INVALID_ASSOCIATED_USER}";
                throw new ServerException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), message);
            }

            if (emailId == null || emailId == Guid.Empty)
            {
                var message = $"Cannot verify SMTP service. Reason: {ErrorCodes.INVALID_ASSOCIATED_EMAIL}";
                throw new ServerException(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL), message);
            }
        }
    }
}