using EmailSender.Backend.Domain.Enums;

namespace EmailSender.Services.UserService;

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Models;

public interface IUserService
{
    string GetPrivateKeyFromHeader(string headerName = "X-Private-Key");

    Task<bool> IsIpAddressAllowed(IPAddress domainName, CancellationToken cancellationToken = default);

    Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken = default);

    Task<Guid> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken = default);

    Task<UserRole?> GetUserRoleByPrivateKey(string privateKey, CancellationToken cancellationToken = default);

    Task<Guid> RegisterUserApiRequest(string requestName, CancellationToken cancellationToken = default);

    Task<UserCredentials> AddUser(UserData userData, CancellationToken cancellationToken = default);

    Task UpdateUser(UserInfo userInfo, CancellationToken cancellationToken = default);

    Task RemoveUser(Guid userId, bool softDelete = false, CancellationToken cancellationToken = default);

    Task UpdateUserDetails(UserCompanyInfo userCompanyInfo, CancellationToken cancellationToken = default);

    Task AddUserEmail(Guid userId, Guid emailId, CancellationToken cancellationToken = default);

    Task UpdateUserEmail(Guid oldEmailId, Guid newEmailId, CancellationToken cancellationToken = default);

    Task RemoveUserEmail(Guid userId, Guid emailId, CancellationToken cancellationToken = default);
}