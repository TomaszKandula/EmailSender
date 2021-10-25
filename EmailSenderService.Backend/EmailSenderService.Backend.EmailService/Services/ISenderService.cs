namespace EmailSenderService.Backend.EmailService.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Domain.Entities;

    public interface ISenderService
    {
        Task<bool> IsDomainAllowed(string domainName, CancellationToken cancellationToken);

        Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken);

        Task<User> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken);

        Task<Guid> VerifyEmailFrom(string emailFrom, Guid userId, CancellationToken cancellationToken);

        Task Send(Configuration configuration, CancellationToken cancellationToken);
    }
}