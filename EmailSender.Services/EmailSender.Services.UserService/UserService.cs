namespace EmailSender.Services.UserService;

using System;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;
using Backend.Database;
using Backend.Domain.Enums;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;
using Backend.Core.Services.LoggerService;
using Backend.Core.Services.DateTimeService;

public class UserService : IUserService
{
    private readonly DatabaseContext _databaseContext;

    private readonly IDateTimeService _dateTimeService;

    private readonly ILoggerService _loggerService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(DatabaseContext databaseContext, ILoggerService loggerService, 
        IHttpContextAccessor httpContextAccessor, IDateTimeService dateTimeService)
    {
        _databaseContext = databaseContext;
        _loggerService = loggerService;
        _httpContextAccessor = httpContextAccessor;
        _dateTimeService = dateTimeService;
    }

    /// <summary>
    /// Returns private key presented in the request header or an empty string.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetPrivateKeyFromHeader(string headerName = "X-Private-Key")
    {
        return _httpContextAccessor.HttpContext?.Request.Headers[headerName].ToString();
    }

    /// <summary>
    /// Checks if given IP address is registered within the system.
    /// It should not contain a scheme, but it may contain a port number.
    /// </summary>
    /// <param name="ipAddress">IP Address.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True or False.</returns>
    public async Task<bool> IsIpAddressAllowed(IPAddress ipAddress, CancellationToken cancellationToken = default)
    {
        var address = ipAddress.ToString();
        var allowedIp = await _databaseContext.UserAllowedIps
            .AsNoTracking()
            .Where(ips => ips.IpAddress == address)
            .SingleOrDefaultAsync(cancellationToken);

        if (allowedIp is null) 
            _loggerService.LogWarning($"IP address '{address}' is not registered within the system.");

        return allowedIp is not null;
    }

    /// <summary>
    /// Checks if a given private key is registered within the system.
    /// </summary>
    /// <param name="privateKey">Private key (alphanumerical).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True or False.</returns>
    public async Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken = default)
    {
        var key = await _databaseContext.Users
            .AsNoTracking()
            .Where(users => users.PrivateKey == privateKey)
            .Where(users => !users.IsDeleted)
            .SingleOrDefaultAsync(cancellationToken);

        if (key is null)
        {
            _loggerService.LogWarning($"Key '{privateKey}' is not registered within the system.");
            return false;
        }

        if (key.Status == UserStatus.Activated) return true;

        _loggerService.LogWarning($"Key '{privateKey}' is not activated within the system.");
        return false;
    }

    /// <summary>
    /// Returns user ID registered for a given private key within the system.
    /// </summary>
    /// <param name="privateKey">Private key (alphanumerical).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User ID.</returns>
    public async Task<Guid> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken = default)
    {
        return await _databaseContext.Users
            .AsNoTracking()
            .Where(user => user.PrivateKey == privateKey)
            .Where(users => users.Status == UserStatus.Activated)
            .Where(users => !users.IsDeleted)
            .Select(user => user.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Returns assigned user role.
    /// </summary>
    /// <param name="privateKey">Private key (alphanumerical).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User ID.</returns>
    public async Task<UserRole?> GetUserRoleByPrivateKey(string privateKey, CancellationToken cancellationToken = default)
    {
        return await _databaseContext.Users
            .AsNoTracking()
            .Where(users => users.PrivateKey == privateKey)
            .Where(users => users.Status == UserStatus.Activated)
            .Where(users => !users.IsDeleted)
            .Select(user => user.Role)
            .SingleOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Register user billable API request.
    /// </summary>
    /// <param name="requestName">Request name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Requester user ID.</returns>
    public async Task<Guid> RegisterUserApiRequest(string requestName, CancellationToken cancellationToken = default)
    {
        var key = GetPrivateKeyFromHeader();
        var userId = await GetUserByPrivateKey(key, cancellationToken);

        var apiRequest = new RequestsHistory
        {
            UserId = userId,
            RequestedAt = _dateTimeService.Now,
            RequestName = requestName
        };

        await _databaseContext.AddAsync(apiRequest, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        return userId;
    }

    /// <summary>
    /// Adds new user for given email address, name and surname.
    /// </summary>
    /// <param name="input">Input data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when email address already exist.</exception>
    /// <returns>Generated API key and basic user information.</returns>
    public async Task<AddUserOutput> AddUser(AddUserInput input, CancellationToken cancellationToken = default)
    {
        var doesEmailExist = await _databaseContext.Users
            .AsNoTracking()
            .Where(users => users.EmailAddress == input.EmailAddress)
            .FirstOrDefaultAsync(cancellationToken) != null;

        if (doesEmailExist)
            throw new BusinessException(nameof(ErrorCodes.USER_EMAIL_ALREADY_EXIST), ErrorCodes.USER_EMAIL_ALREADY_EXIST);

        const UserStatus userStatus = UserStatus.PendingActivation;
        var privateKey = Guid.NewGuid().ToString("N");
        var userAlias = $"{input.FirstName[..2]}{input.LastName[..3]}".ToLower();

        var newUser = new Users
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            UserAlias = userAlias,
            EmailAddress = input.EmailAddress,
            Status = userStatus,
            Registered = _dateTimeService.Now,
            PrivateKey = privateKey,
            IsDeleted = false,
            Role = UserRole.OrdinaryUser,
        };

        await _databaseContext.Users.AddAsync(newUser, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new AddUserOutput
        {
            UserId = newUser.Id,
            PrivateKey = privateKey,
            UserAlias = userAlias,
            EmailAddress = input.EmailAddress,
            Status = userStatus
        };
    }

    /// <summary>
    /// Updates current user basic info.
    /// </summary>
    /// <param name="input">Input data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when email address already exists or a user does not exist.</exception>
    public async Task UpdateUser(UpdateUserInput input, CancellationToken cancellationToken = default)
    {
        var user = await GetActiveUser(input.UserId, true, cancellationToken);
        var isEmailPresent = await _databaseContext.Users
            .SingleOrDefaultAsync(users => users.EmailAddress == input.EmailAddress, cancellationToken) != null;

        if (isEmailPresent)
            throw new BusinessException(nameof(ErrorCodes.USER_EMAIL_ALREADY_EXIST), ErrorCodes.USER_EMAIL_ALREADY_EXIST);

        user.FirstName = input.FirstName;
        user.LastName = input.LastName;
        user.EmailAddress = input.EmailAddress;

        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Removes given user or hides if soft delete is enabled.
    /// </summary>
    /// <param name="input">User ID and flag to enable/disable soft delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task RemoveUser(RemoveUserInput input, CancellationToken cancellationToken = default)
    {
        var user = await GetActiveUser(input.UserId, true, cancellationToken);
        if (input.SoftDelete)
        {
            user.IsDeleted = true;
        }
        else
        {
            _databaseContext.Remove(user);
        }

        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates company information assigned to a given user ID.
    /// </summary>
    /// <param name="input">User company information, including VAT etc.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task UpdateUserDetails(UpdateUserDetailsInput input, CancellationToken cancellationToken = default)
    {
        var user = await GetActiveUser(input.UserId, true, cancellationToken);
        var details = await _databaseContext.UserDetails
            .SingleOrDefaultAsync(details => details.UserId == user.Id, cancellationToken);

        if (details is null)
        {
            var newUserDetails = new UserDetails
            {
                UserId = user.Id,
                CompanyName = input.CompanyName,
                VatNumber = input.VatNumber,
                StreetAddress = input.StreetAddress,
                PostalCode = input.PostalCode,
                Country = input.Country,
                City = input.City
            };
        
            await _databaseContext.UserDetails.AddAsync(newUserDetails, cancellationToken);
        }
        else
        {
            details.CompanyName = input.CompanyName;
            details.VatNumber = input.VatNumber;
            details.StreetAddress = input.StreetAddress;
            details.PostalCode = input.PostalCode;
            details.Country = input.Country;
            details.City = input.City;
        }

        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Adds associated email address by ID.
    /// </summary>
    /// <param name="input">User ID and new email address ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task AddUserEmail(AddUserEmailInput input, CancellationToken cancellationToken = default)
    {
        var user = await GetActiveUser(input.UserId, true, cancellationToken);
        var isEmailPresent = await _databaseContext.Emails
            .SingleOrDefaultAsync(emails => emails.Id == input.EmailId, cancellationToken) != null;

        if (!isEmailPresent)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL), ErrorCodes.INVALID_ASSOCIATED_EMAIL);

        var newUserEmail = new UserEmails
        {
            UserId = user.Id,
            EmailId = input.EmailId
        };

        await _databaseContext.UserEmails.AddAsync(newUserEmail, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates associated email address by ID.
    /// </summary>
    /// <param name="input">Current associated user email ID and new associated email address ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when associated user email does not exist.</exception>
    public async Task UpdateUserEmail(UpdateUserEmailInput input, CancellationToken cancellationToken = default)
    {
        var user = await GetActiveUser(input.UserId, true, cancellationToken);
        var userEmails = await _databaseContext.UserEmails
            .Where(emails => emails.Id == input.OldEmailId)
            .Where(emails => emails.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEmails is null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ID), ErrorCodes.INVALID_ID);

        userEmails.EmailId = input.NewEmailId;
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Removes associated email address by ID.
    /// </summary>
    /// <param name="input">User ID and current associated email address ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when associated user email does not exist.</exception>
    public async Task RemoveUserEmail(RemoveUserEmailInput input, CancellationToken cancellationToken = default)
    {
        var user = await GetActiveUser(input.UserId, true, cancellationToken);
        var userEmails = await _databaseContext.UserEmails
            .Where(emails => emails.EmailId == input.EmailId)
            .Where(emails => emails.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (userEmails is null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_EMAIL_ID), ErrorCodes.INVALID_EMAIL_ID);

        _databaseContext.Remove(userEmails);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Returns user ID and assigned user role.
    /// </summary>
    /// <remarks>
    /// It accepts user ID that belongs to other user. This require Administrator role. 
    /// </remarks>
    /// <param name="otherUserId">Optional user ID.</param>
    /// <param name="asTracking"></param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="AccessException">
    /// Throws an exception whenever a user tries to modify another user while having
    /// no administrator privileges.
    /// </exception>
    /// <returns>User ID and user role.</returns>
    private async Task<Users> GetActiveUser(Guid? otherUserId, bool asTracking = false, CancellationToken cancellationToken = default)
    {
        var privateKey = GetPrivateKeyFromHeader();
        var entity = asTracking ? _databaseContext.Users : _databaseContext.Users.AsNoTracking();
        var user = await entity
            .Where(users => users.PrivateKey == privateKey)
            .Where(users => users.Status == UserStatus.Activated)
            .Where(users => !users.IsDeleted)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_PRIVATE_KEY), ErrorCodes.INVALID_PRIVATE_KEY);

        if (otherUserId is null) return user;

        var otherUser = await entity
            .Where(users => users.Id == otherUserId)
            .Where(users => users.Status == UserStatus.Activated)
            .Where(users => !users.IsDeleted)
            .SingleOrDefaultAsync(cancellationToken);

        if (otherUser is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXIST), ErrorCodes.USER_DOES_NOT_EXIST);

        if (user.Role == UserRole.OrdinaryUser && user.Id != otherUserId)
            throw new AccessException(nameof(ErrorCodes.INSUFFICIENT_PRIVILEGES), ErrorCodes.INSUFFICIENT_PRIVILEGES);

        return otherUser;
    }
}