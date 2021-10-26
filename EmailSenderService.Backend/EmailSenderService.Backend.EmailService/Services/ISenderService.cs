#nullable enable
namespace EmailSenderService.Backend.EmailService.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using SmtpService.Models;

    public interface ISenderService
    {
        Task<bool> IsDomainAllowed(string domainName, CancellationToken cancellationToken);

        Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken);

        Task<Guid> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken);

        Task<Guid> VerifyEmailFrom(string emailFrom, Guid? userId, CancellationToken cancellationToken);

        Task Send(Configuration configuration, CancellationToken cancellationToken);

        Task<ErrorResult?> VerifyConnection(Guid emailId, CancellationToken cancellationToken);
        
        Task<IEnumerable<VerifyEmail>> VerifyEmailAddress(IEnumerable<string> emailAddress, CancellationToken cancellationToken);
    }
}