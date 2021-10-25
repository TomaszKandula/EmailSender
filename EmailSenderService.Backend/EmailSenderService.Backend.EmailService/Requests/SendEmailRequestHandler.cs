namespace EmailSenderService.Backend.EmailService.Requests
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Services;
    using Database;
    using Domain.Entities;
    using Shared.Resources;
    using Shared.Exceptions;
    using Shared.Services.DateTimeService;

    public class SendEmailRequestHandler : TemplateHandler<SendEmailRequest, Unit>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly ISenderService _senderService;

        private readonly IDateTimeService _dateTimeService;

        public SendEmailRequestHandler(DatabaseContext databaseContext, ISenderService senderService, 
            IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _senderService = senderService;
            _dateTimeService = dateTimeService;
        }

        public override async Task<Unit> Handle(SendEmailRequest request, CancellationToken cancellationToken)
        {
            var isKeyValid = await _senderService.IsPrivateKeyValid(request.PrivateKey, cancellationToken);
            var user = await _senderService.GetUserByPrivateKey(request.PrivateKey, cancellationToken);
            var emailId = await _senderService.VerifyEmailFrom(request.From, user.Id, cancellationToken);

            VerifyArguments(isKeyValid, user, emailId);
            
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

            var history = new History
            {
                UserId = user.Id,
                EmailId = emailId,
                Sent = _dateTimeService.Now
            };

            await _databaseContext.History.AddAsync(history, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }

        private static void VerifyArguments(bool isKeyValid, User user, Guid emailId)
        {
            if (!isKeyValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);

            if (user == null)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_USER), ErrorCodes.INVALID_ASSOCIATED_USER);

            if (emailId == Guid.Empty)
                throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL), ErrorCodes.INVALID_ASSOCIATED_EMAIL);
        }
    }
}