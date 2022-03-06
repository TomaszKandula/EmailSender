namespace EmailSender.Services.UserService;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;
using Backend.Database;
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
    /// Returns private key presented in the request header or empty string.
    /// </summary>
    /// <returns>String value.</returns>
    public string GetPrivateKeyFromHeader(string headerName = "X-Private-Key")
    {
        return _httpContextAccessor.HttpContext?.Request.Headers[headerName].ToString();
    }

    /// <summary>
    /// Checks if given domain name is registered within the system. It should not contain scheme,
    /// but it may contain port number.
    /// </summary>
    /// <param name="domainName">Domain name without scheme, but it may have port.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True or False.</returns>
    public async Task<bool> IsDomainAllowed(string domainName, CancellationToken cancellationToken = default)
    {
        var domains = await _databaseContext.UserDomains
            .AsNoTracking()
            .Where(allowDomain => allowDomain.Host == domainName)
            .ToListAsync(cancellationToken);

        var isDomainAllowed = domains.Any();
        if (!isDomainAllowed) 
            _loggerService.LogWarning($"Domain '{domainName}' is not registered within the system.");

        return isDomainAllowed;
    }

    /// <summary>
    /// Checks if given private key is registered within the system.
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True or False.</returns>
    public async Task<bool> IsPrivateKeyValid(string privateKey, CancellationToken cancellationToken = default)
    {
        var keys = await _databaseContext.Users
            .AsNoTracking()
            .Where(user => user.PrivateKey == privateKey)
            .ToListAsync(cancellationToken);

        var isPrivateKeyExists = keys.Any();
        if (!isPrivateKeyExists)
            _loggerService.LogWarning($"Key '{privateKey}' is not registered within the system.");
            
        return isPrivateKeyExists;
    }

    /// <summary>
    /// Returns user ID registered for given private key within the system.
    /// </summary>
    /// <param name="privateKey">Private key (alphanumerical).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User ID (Guid).</returns>
    public async Task<Guid> GetUserByPrivateKey(string privateKey, CancellationToken cancellationToken = default)
    {
        return await _databaseContext.Users
            .AsNoTracking()
            .Where(user => user.PrivateKey == privateKey)
            .Select(user => user.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Adds new user for given email address, name and surname.
    /// </summary>
    /// <param name="userData">Input data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when email address already exists.</exception>
    /// <returns>Generated API key and user alias.</returns>
    public async Task<UserCredentials> AddUser(UserData userData, CancellationToken cancellationToken = default)
    {
        var doesEmailExist = await _databaseContext.Users
            .AsNoTracking()
            .Where(users => users.EmailAddress == userData.EmailAddress)
            .FirstOrDefaultAsync(cancellationToken) != null;

        if (doesEmailExist)
            throw new BusinessException(nameof(ErrorCodes.USER_EMAIL_ALREADY_EXISTS), ErrorCodes.USER_EMAIL_ALREADY_EXISTS);

        var privateKey = Guid.NewGuid().ToString("N");
        var userAlias = $"{userData.FirstName[..2]}{userData.LastName[..3]}";
        var newUser = new Users
        {
            FirstName = userData.FirstName,
            LastName = userData.LastName,
            UserAlias = userAlias,
            EmailAddress = userData.EmailAddress,
            IsActivated = true,
            Registered = _dateTimeService.Now,
            PrivateKey = privateKey
        };

        await _databaseContext.Users.AddAsync(newUser, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new UserCredentials
        {
            PrivateKey = privateKey,
            UserAlias = userAlias,
            EmailAddress = userData.EmailAddress
        };
    }

    /// <summary>
    /// Updates current user basic info.
    /// </summary>
    /// <param name="userInfo">Input data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when email address already exists or user does not exist.</exception>
    public async Task UpdateUser(UserInfo userInfo, CancellationToken cancellationToken = default)
    {
        var doesEmailExist = await _databaseContext.Users
            .AsNoTracking()
            .Where(users => users.EmailAddress == userInfo.EmailAddress)
            .FirstOrDefaultAsync(cancellationToken) != null;

        if (doesEmailExist)
            throw new BusinessException(nameof(ErrorCodes.USER_EMAIL_ALREADY_EXISTS), ErrorCodes.USER_EMAIL_ALREADY_EXISTS);

        var currentUser = await _databaseContext.Users
            .Where(users => users.Id == userInfo.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (currentUser == null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        currentUser.FirstName = userInfo.FirstName;
        currentUser.LastName = userInfo.LastName;
        currentUser.EmailAddress = userInfo.EmailAddress;

        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Removes given user or hides if soft delete is enabled.
    /// </summary>
    /// <param name="userId">User ID (Guid).</param>
    /// <param name="softDelete">Enable/disable soft delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when user does not exist.</exception>
    public async Task RemoveUser(Guid userId, bool softDelete = false, CancellationToken cancellationToken = default)
    {
        var currentUser = await _databaseContext.Users
            .Where(users => users.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        if (currentUser == null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        if (softDelete)
        {
            currentUser.IsDeleted = true;
        }
        else
        {
            _databaseContext.Remove(currentUser);
        }

        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates company information assigned to given user ID.
    /// </summary>
    /// <param name="userCompanyInfo">User company information, including VAT etc.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when user does not exist.</exception>
    public async Task UpdateUserDetails(UserCompanyInfo userCompanyInfo, CancellationToken cancellationToken = default)
    {
        var doesUserExist = await _databaseContext.Users
            .AsNoTracking()
            .Where(users => users.Id == userCompanyInfo.UserId)
            .FirstOrDefaultAsync(cancellationToken) != null;

        if (!doesUserExist)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var userDetails = await _databaseContext.UserDetails
            .Where(details => details.UserId == userCompanyInfo.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (userDetails == null)
        {
            var newUserDetails = new UserDetails
            {
                UserId = userCompanyInfo.UserId,
                CompanyName = userCompanyInfo.CompanyName,
                VatNumber = userCompanyInfo.VatNumber,
                StreetAddress = userCompanyInfo.StreetAddress,
                PostalCode = userCompanyInfo.PostalCode,
                Country = userCompanyInfo.Country,
                City = userCompanyInfo.City
            };
        
            await _databaseContext.UserDetails.AddAsync(newUserDetails, cancellationToken);
        }
        else
        {
            userDetails.CompanyName = userCompanyInfo.CompanyName;
            userDetails.VatNumber = userCompanyInfo.VatNumber;
            userDetails.StreetAddress = userCompanyInfo.StreetAddress;
            userDetails.PostalCode = userCompanyInfo.PostalCode;
            userDetails.Country = userCompanyInfo.Country;
            userDetails.City = userCompanyInfo.City;
        }

        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Adds associated email address by ID.
    /// </summary>
    /// <param name="userId">User ID (Guid).</param>
    /// <param name="emailId">Associated email address ID (Guid).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when user/associated email does not exist.</exception>
    public async Task AddUserEmail(Guid userId, Guid emailId, CancellationToken cancellationToken = default)
    {
        var doesUserExist = await _databaseContext.Users
            .AsNoTracking()
            .Where(users => users.Id == userId)
            .FirstOrDefaultAsync(cancellationToken) != null;

        if (!doesUserExist)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var doesEmailExist = await _databaseContext.Emails
            .Where(emails => emails.Id == emailId)
            .FirstOrDefaultAsync(cancellationToken) != null;

        if (!doesEmailExist)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL), ErrorCodes.INVALID_ASSOCIATED_EMAIL);

        var newUserEmail = new UserEmails
        {
            UserId = userId,
            EmailId = emailId
        };

        await _databaseContext.UserEmails.AddAsync(newUserEmail, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates associated email address by ID.
    /// </summary>
    /// <param name="id">Associated user email ID (Guid).</param>
    /// <param name="newEmailId">New associated email address ID (Guid).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when user/associated email does not exist.</exception>
    public async Task UpdateUserEmail(Guid id, Guid newEmailId, CancellationToken cancellationToken = default)
    {
        var userEmails = await _databaseContext.UserEmails
            .Where(emails => emails.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (userEmails == null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ID), ErrorCodes.INVALID_ID);

        userEmails.EmailId = newEmailId;
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Removes associated email address by ID.
    /// </summary>
    /// <param name="userId">User ID (Guid).</param>
    /// <param name="emailId">Associated email address ID (Guid).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <exception cref="BusinessException">Throws an exception when user/associated email does not exist.</exception>
    public async Task RemoveUserEmail(Guid userId, Guid emailId, CancellationToken cancellationToken = default)
    {
        var userEmails = await _databaseContext.UserEmails
            .Where(emails => emails.UserId == userId && emails.EmailId == emailId)
            .FirstOrDefaultAsync(cancellationToken);

        if (userEmails == null)
            throw new BusinessException(nameof(ErrorCodes.USER_ID_OR_EMAIL_ID_INVALID), ErrorCodes.USER_ID_OR_EMAIL_ID_INVALID);

        _databaseContext.Remove(userEmails);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}