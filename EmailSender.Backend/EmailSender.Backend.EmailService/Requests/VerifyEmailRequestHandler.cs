namespace EmailSender.Backend.EmailService.Requests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Services;
    using Responses;
    using Shared.Resources;
    using Shared.Exceptions;

    public class VerifyEmailRequestHandler : TemplateHandler<VerifyEmailRequest, VerifyEmailResponse>
    {
        private readonly ISenderService _senderService;

        public VerifyEmailRequestHandler(ISenderService senderService) => _senderService = senderService;

        public override async Task<VerifyEmailResponse> Handle(VerifyEmailRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var userId = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);

            VerifyArguments(isKeyValid, userId);

            var result = await _senderService.VerifyEmailAddress(request.Emails, cancellationToken);

            return new VerifyEmailResponse
            {
                CheckResult = result
            };
        }

        private static void VerifyArguments(bool isKeyValid, Guid? userId)
        {
            if (!isKeyValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);

            if (userId == null || userId == Guid.Empty)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);
        }
    }
}