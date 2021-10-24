using System.Threading;

namespace EmailSenderService.Backend.EmailService
{
    using System.Threading.Tasks;
    using Models;

    public interface IEmailService
    {
        Task<bool> IsDomainAllowed(string domainName, CancellationToken cancellationToken);

        Task<bool> IsPrivateKeyExists(string privateKey, CancellationToken cancellationToken);

        Task Send(Configuration configuration, CancellationToken cancellationToken);
    }
}