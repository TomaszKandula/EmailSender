using System.Net;
using EmailSender.Backend.Domain.Enums;
using EmailSender.Services.UserService.Models;

namespace EmailSender.Services.UserService;

public interface IUserService
{
    string GetPrivateKeyFromHeader(string headerName = "X-Private-Key");

    Task<string> GeneratePrivateKey(Guid? userId, CancellationToken cancellationToken = default);

    Task<bool> IsIpAddressAllowed(IPAddress ipAddress, CancellationToken cancellationToken = default);

    Task<bool> IsIpAddressAllowed(string ipAddress, CancellationToken cancellationToken = default);

    Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken = default);

    Task<Guid> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken = default);

    Task<UserRole?> GetUserRoleByPrivateKey(string privateKey, CancellationToken cancellationToken = default);

    Task<Guid> RegisterUserApiRequest(string requestName, CancellationToken cancellationToken = default);

    Task AlterUserStatus(AlterUserStatusInput input, CancellationToken cancellationToken = default);

    Task<AddUserOutput> AddUser(AddUserInput input, CancellationToken cancellationToken = default);

    Task UpdateUser(UpdateUserInput input, CancellationToken cancellationToken = default);

    Task RemoveUser(RemoveUserInput input, CancellationToken cancellationToken = default);

    Task UpdateUserDetails(UpdateUserDetailsInput input, CancellationToken cancellationToken = default);

    Task AddUserEmail(AddUserEmailInput input, CancellationToken cancellationToken = default);

    Task UpdateUserEmail(UpdateUserEmailInput input, CancellationToken cancellationToken = default);

    Task RemoveUserEmail(RemoveUserEmailInput input, CancellationToken cancellationToken = default);
}