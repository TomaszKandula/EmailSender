namespace EmailSenderService.Backend.EmailService
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Database;
    using SmtpService;
    using Shared.Resources;
    using Shared.Exceptions;
    using Shared.Services.LoggerService;

    public class EmailService : IEmailService
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ISmtpClientService _smtpClientService;

        private readonly ILoggerService _loggerService;
        
        public EmailService(DatabaseContext databaseContext, ISmtpClientService smtpClientService, ILoggerService loggerService)
        {
            _databaseContext = databaseContext;
            _smtpClientService = smtpClientService;
            _loggerService = loggerService;
        }

        public async Task<bool> IsDomainAllowed(string domainName, CancellationToken cancellationToken)
        {
            var domains = await _databaseContext.AllowDomain
                .Where(allowDomain => allowDomain.Host == domainName)
                .ToListAsync(cancellationToken);

            var isDomainAllowed = domains.Any();
            if (!isDomainAllowed) 
                _loggerService.LogWarning($"Domain '{domainName}' is not registered within the system, thus cannot be allowed to send an email.");

            return isDomainAllowed;
        }

        public async Task<bool> IsPrivateKeyExists(string privateKey, CancellationToken cancellationToken)
        {
            var keys = await _databaseContext.User
                .Where(user => user.PrivateKey == privateKey)
                .ToListAsync(cancellationToken);

            var isPrivateKeyExists = keys.Any();
            if (!isPrivateKeyExists)
                _loggerService.LogWarning($"Key '{privateKey}' is not registered within the system, request will be rejected.");
            
            return isPrivateKeyExists;
        }

        public async Task Send(Configuration configuration, CancellationToken cancellationToken)
        {
            VerifyRequiredConfiguration(configuration);
            VerifyOptionalConfiguration(configuration);
            
            _smtpClientService.From = configuration.From;
            _smtpClientService.Tos = configuration.To;
            _smtpClientService.Ccs = configuration.Cc;
            _smtpClientService.Bccs = configuration.Bcc;
            _smtpClientService.Subject = configuration.Subject;

            if (configuration.IsHtml)
            {
                _smtpClientService.HtmlBody = configuration.Body;
            }
            else
            {
                _smtpClientService.PlainText = configuration.Body;
            }

            var result = await _smtpClientService.Send(cancellationToken);
            if (!result.IsSucceeded)
            {
                var message = result.InnerMessage == string.Empty ? result.ErrorDesc : result.InnerMessage;
                throw new BusinessException(result.ErrorCode, message);
            }
        }

        private void VerifyRequiredConfiguration(Configuration configuration)
        {
            if (string.IsNullOrEmpty(configuration.From))
                throw new BusinessException(nameof(ErrorCodes.INVALID_ARGUMENT), ErrorCodes.INVALID_ARGUMENT);

            if (string.IsNullOrEmpty(configuration.Subject))
                throw new BusinessException(nameof(ErrorCodes.INVALID_ARGUMENT), ErrorCodes.INVALID_ARGUMENT);

            var verifyTo = _smtpClientService.IsAddressCorrect(configuration.To);
            if (verifyTo.Select(email => email.IsValid).Contains(false))
                throw new BusinessException(nameof(ErrorCodes.INVALID_EMAIL_FORMAT), ErrorCodes.INVALID_EMAIL_FORMAT);
        }

        private void VerifyOptionalConfiguration(Configuration configuration)
        {
            if (configuration.Cc != null && configuration.Cc.Any())
            {
                var verifyCc = _smtpClientService.IsAddressCorrect(configuration.Cc);
                if (verifyCc.Select(email => email.IsValid).Contains(false))
                    throw new BusinessException(nameof(ErrorCodes.INVALID_EMAIL_FORMAT), ErrorCodes.INVALID_EMAIL_FORMAT);
            }

            if (configuration.Bcc != null && configuration.Bcc.Any())
            {
                var verifyBcc = _smtpClientService.IsAddressCorrect(configuration.Bcc);
                if (verifyBcc.Select(email => email.IsValid).Contains(false))
                    throw new BusinessException(nameof(ErrorCodes.INVALID_EMAIL_FORMAT), ErrorCodes.INVALID_EMAIL_FORMAT);
            }
        }
    }
}