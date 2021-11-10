namespace EmailSender.Backend.UserService
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<bool> IsDomainAllowed(string domainName, CancellationToken cancellationToken);

        Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken);

        Task<Guid> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken);
    }
}