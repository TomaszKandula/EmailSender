namespace EmailSender.Services.UserService;

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Enums;
using Models;

public interface IUserService
{
    string GetPrivateKeyFromHeader(string headerName = "X-Private-Key");

    string GeneratePrivateKey();

    Task<bool> IsIpAddressAllowed(IPAddress domainName, CancellationToken cancellationToken = default);

    Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken = default);

    Task<Guid> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken = default);

    Task<UserRole?> GetUserRoleByPrivateKey(string privateKey, CancellationToken cancellationToken = default);

    Task<Guid> RegisterUserApiRequest(string requestName, CancellationToken cancellationToken = default);

    Task<AddUserOutput> AddUser(AddUserInput input, CancellationToken cancellationToken = default);

    Task UpdateUser(UpdateUserInput input, CancellationToken cancellationToken = default);

    Task RemoveUser(RemoveUserInput input, CancellationToken cancellationToken = default);

    Task UpdateUserDetails(UpdateUserDetailsInput input, CancellationToken cancellationToken = default);

    Task AddUserEmail(AddUserEmailInput input, CancellationToken cancellationToken = default);

    Task UpdateUserEmail(UpdateUserEmailInput input, CancellationToken cancellationToken = default);

    Task RemoveUserEmail(RemoveUserEmailInput input, CancellationToken cancellationToken = default);
}