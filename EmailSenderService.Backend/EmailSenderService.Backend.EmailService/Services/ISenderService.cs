namespace EmailSenderService.Backend.EmailService.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface ISenderService
    {
        Task<bool> IsDomainAllowed(string domainName, CancellationToken cancellationToken);

        Task<bool> IsPrivateKeyExists(string privateKey, CancellationToken cancellationToken);

        Task Send(Configuration configuration, CancellationToken cancellationToken);
    }
}