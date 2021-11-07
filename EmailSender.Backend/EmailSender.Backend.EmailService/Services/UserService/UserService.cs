namespace EmailSender.Backend.EmailService.Services.UserService
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Shared.Services.LoggerService;

    public class UserService : IUserService
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ILoggerService _loggerService;

        public UserService(DatabaseContext databaseContext, ILoggerService loggerService)
        {
            _databaseContext = databaseContext;
            _loggerService = loggerService;
        }

        public async Task<bool> IsDomainAllowed(string domainName, CancellationToken cancellationToken)
        {
            var domains = await _databaseContext.AllowDomain
                .AsNoTracking()
                .Where(allowDomain => allowDomain.Host == domainName)
                .ToListAsync(cancellationToken);

            var isDomainAllowed = domains.Any();
            if (!isDomainAllowed) 
                _loggerService.LogWarning($"Domain '{domainName}' is not registered within the system.");

            return isDomainAllowed;
        }

        public async Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken)
        {
            var keys = await _databaseContext.User
                .AsNoTracking()
                .Where(user => user.PrivateKey == privateKey)
                .ToListAsync(cancellationToken);

            var isPrivateKeyExists = keys.Any();
            if (!isPrivateKeyExists)
                _loggerService.LogWarning($"Key '{privateKey}' is not registered within the system.");
            
            return isPrivateKeyExists;
        }

        public async Task<Guid> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken)
        {
            return await _databaseContext.User
                .AsNoTracking()
                .Where(user => user.PrivateKey == privateKey)
                .Select(user => user.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}