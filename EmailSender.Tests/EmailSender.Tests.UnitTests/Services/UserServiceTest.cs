namespace EmailSender.Tests.UnitTests.Services;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;
using EmailSender.Services.UserService;
using Backend.Core.Services.LoggerService;
using Backend.Core.Services.DateTimeService;
using EmailSender.Services.UserService.Models;

public class UserServiceTest : TestBase
{
    [Fact]
    public async Task GivenDomainName_WhenInvokeIsDomainAllowed_ShouldSucceed()
    {
        // Arrange
        var domainName = DataUtilityService.GetRandomString(useAlphabetOnly: true);

        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };
            
        var allowDomain = new UserDomains
        {
            UserId = user.Id,
            Host = domainName
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(allowDomain);
        await databaseContext.SaveChangesAsync();
            
        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();
        var service = new UserService(
            databaseContext,
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        var result = await service.IsDomainAllowed(domainName, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task GivenIncorrectDomainName_WhenInvokeIsDomainAllowed_ShouldFail()
    {
        // Arrange
        var domainName = DataUtilityService.GetRandomString(useAlphabetOnly: true);

        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };
            
        var allowDomain = new UserDomains
        {
            UserId = user.Id,
            Host = DataUtilityService.GetRandomString(useAlphabetOnly: true)
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(allowDomain);
        await databaseContext.SaveChangesAsync();
            
        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();
        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        var result = await service.IsDomainAllowed(domainName, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GivenPrivateKey_WhenInvokeIsPrivateKeyValid_ShouldSucceed()
    {
        // Arrange
        var privateKey = DataUtilityService.GetRandomString();
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = privateKey
        };
            
        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();
            
        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();
        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        var result = await service.IsPrivateKeyValid(privateKey, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task GivenIncorrectPrivateKey_WhenInvokeIsPrivateKeyValid_ShouldFail()
    {
        // Arrange
        var privateKey = DataUtilityService.GetRandomString();
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };
            
        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();
            
        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();
        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        var result = await service.IsPrivateKeyValid(privateKey, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GivenPrivateKey_WhenGetUserByPrivateKey_ShouldSucceed()
    {
        // Arrange
        var privateKey = DataUtilityService.GetRandomString();
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = privateKey
        };
            
        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();
            
        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();
        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        var result = await service.GetUserByPrivateKey(privateKey, CancellationToken.None);

        // Assert
        (result == Guid.Empty).Should().BeFalse();
    }

    [Fact]
    public async Task GivenIncorrectPrivateKey_WhenGetUserByPrivateKey_ShouldFail()
    {
        // Arrange
        var privateKey = DataUtilityService.GetRandomString();
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };
            
        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();
            
        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();
        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        var result = await service.GetUserByPrivateKey(privateKey, CancellationToken.None);

        // Assert
        (result == Guid.Empty).Should().BeTrue();
    }

    [Fact]
    public async Task GivenUserData_WhenAddUser_ShouldReturnObject()
    {
        // Arrange
        var userData = new UserData
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            EmailAddress = DataUtilityService.GetRandomEmail()
        };

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();
        var databaseContext = GetTestDatabaseContext();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        var result = await service.AddUser(userData);

        // Assert
        result.PrivateKey.Should().NotBeEmpty();
        result.PrivateKey.Should().HaveLength(32);
        result.EmailAddress.Should().Be(userData.EmailAddress);
        result.UserAlias.Should().Be($"{userData.FirstName[..2]}{userData.LastName[..3]}");
    }

    [Fact]
    public async Task GivenUserDataWithExistingEmail_WhenAddUser_ShouldThrowError()
    {
        // Arrange
        var existingUser = new Users
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName  = DataUtilityService.GetRandomString(),
            UserAlias  = DataUtilityService.GetRandomString(5),
            EmailAddress  = DataUtilityService.GetRandomEmail(),
            IsActivated  = true,
            IsDeleted  = false,
            Registered  = DateTimeService.Now,
            PrivateKey  = Guid.NewGuid().ToString("N"),
        };

        var userData = new UserData
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            EmailAddress = existingUser.EmailAddress
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(existingUser);
        await databaseContext.SaveChangesAsync();
        
        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.AddUser(userData));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_EMAIL_ALREADY_EXISTS));
    }
    
    [Fact]
    public async Task GivenNewDataAndExistingData_WhenUpdateUser_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var newUserData = new UserInfo
        {
            UserId = user.Id,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            EmailAddress = DataUtilityService.GetRandomEmail()
        };

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        await service.UpdateUser(newUserData);
        var data = await databaseContext.Users
            .Where(users => users.Id == user.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().NotBeNull();
        data.FirstName.Should().Be(newUserData.FirstName);
        data.LastName.Should().Be(newUserData.LastName);
        data.EmailAddress.Should().Be(newUserData.EmailAddress);
    }

    [Fact]
    public async Task GivenNewDataAndExistingDataWithSameEmail_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var user = new List<Users>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                UserAlias = DataUtilityService.GetRandomString(5),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Registered = DateTimeService.Now.AddDays(-120),
                IsActivated = true,
                PrivateKey = DataUtilityService.GetRandomString()
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                UserAlias = DataUtilityService.GetRandomString(5),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Registered = DateTimeService.Now.AddDays(-120),
                IsActivated = true,
                PrivateKey = DataUtilityService.GetRandomString()
            },
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(user);
        await databaseContext.SaveChangesAsync();

        var newUserData = new UserInfo
        {
            UserId = user[0].Id,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            EmailAddress = user[1].EmailAddress
        };

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateUser(newUserData));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_EMAIL_ALREADY_EXISTS));
    }

    [Fact]
    public async Task GivenIncorrectUserId_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(user);
        await databaseContext.SaveChangesAsync();

        var newUserData = new UserInfo
        {
            UserId = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            EmailAddress = DataUtilityService.GetRandomEmail()
        };

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateUser(newUserData));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXISTS));
    }

    [Fact]
    public async Task GivenIncorrectUserId_WhenRemoveUser_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var userId = Guid.NewGuid();
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.RemoveUser(userId));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXISTS));
    }

    [Fact]
    public async Task GivenExistingUserAndNoSoftDelete_WhenRemoveUser_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);
        
        // Act
        await service.RemoveUser(user.Id);
        var data = await databaseContext.Users
            .Where(users => users.Id == user.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().BeNull();
    }

    [Fact]
    public async Task GivenExistingUserAndSoftDelete_WhenRemoveUser_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);
        
        // Act
        await service.RemoveUser(user.Id, true);
        var data = await databaseContext.Users
            .Where(users => users.Id == user.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().NotBeNull();
        data.IsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task GivenIncorrectUserId_WhenUpdateUserDetails_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var userCompanyInfo = new UserCompanyInfo
        {
            UserId = Guid.NewGuid(),
            CompanyName = DataUtilityService.GetRandomString(),
            VatNumber = DataUtilityService.GetRandomInteger().ToString(),
            StreetAddress = DataUtilityService.GetRandomString(),
            PostalCode = DataUtilityService.GetRandomInteger().ToString(),
            Country = DataUtilityService.GetRandomString(),
            City = DataUtilityService.GetRandomString()
        };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateUserDetails(userCompanyInfo));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXISTS));
    }

    [Fact]
    public async Task GivenExistingUserAndNoCompanyInfo_WhenUpdateUserDetails_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var userCompanyInfo = new UserCompanyInfo
        {
            UserId = user.Id,
            CompanyName = DataUtilityService.GetRandomString(),
            VatNumber = DataUtilityService.GetRandomInteger().ToString(),
            StreetAddress = DataUtilityService.GetRandomString(),
            PostalCode = DataUtilityService.GetRandomInteger().ToString(),
            Country = DataUtilityService.GetRandomString(),
            City = DataUtilityService.GetRandomString()
        };

        // Act
        await service.UpdateUserDetails(userCompanyInfo);
        var data = await databaseContext.UserDetails
            .Where(details => details.UserId == user.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().NotBeNull();
        data.CompanyName.Should().Be(userCompanyInfo.CompanyName);
        data.VatNumber.Should().Be(userCompanyInfo.VatNumber); 
        data.StreetAddress.Should().Be(userCompanyInfo.StreetAddress); 
        data.PostalCode.Should().Be(userCompanyInfo.PostalCode); 
        data.Country.Should().Be(userCompanyInfo.Country); 
        data.City.Should().Be(userCompanyInfo.City); 
    }

    [Fact]
    public async Task GivenExistingUserAndCompanyInfo_WhenUpdateUserDetails_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var userDetails = new UserDetails
        {
            UserId = user.Id,
            CompanyName = DataUtilityService.GetRandomString(),
            VatNumber = DataUtilityService.GetRandomInteger().ToString(),
            StreetAddress = DataUtilityService.GetRandomString(),
            PostalCode = DataUtilityService.GetRandomInteger().ToString(),
            Country = DataUtilityService.GetRandomString(),
            City = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(userDetails);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var userCompanyInfo = new UserCompanyInfo()
        {
            UserId = user.Id,
            CompanyName = DataUtilityService.GetRandomString(),
            VatNumber = DataUtilityService.GetRandomInteger().ToString(),
            StreetAddress = DataUtilityService.GetRandomString(),
            PostalCode = DataUtilityService.GetRandomInteger().ToString(),
            Country = DataUtilityService.GetRandomString(),
            City = DataUtilityService.GetRandomString()
        };

        // Act
        await service.UpdateUserDetails(userCompanyInfo);
        var data = await databaseContext.UserDetails
            .Where(details => details.UserId == user.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().NotBeNull();
        data.CompanyName.Should().Be(userCompanyInfo.CompanyName);
        data.VatNumber.Should().Be(userCompanyInfo.VatNumber); 
        data.StreetAddress.Should().Be(userCompanyInfo.StreetAddress); 
        data.PostalCode.Should().Be(userCompanyInfo.PostalCode); 
        data.Country.Should().Be(userCompanyInfo.Country); 
        data.City.Should().Be(userCompanyInfo.City); 
    }

    [Fact]
    public async Task GivenExistingUserAndEmail_WhenAddUserEmail_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var email = new Emails
        {
            Address = DataUtilityService.GetRandomEmail(),
            IsActive = true,
            ServerName = DataUtilityService.GetRandomString(),
            ServerKey = DataUtilityService.GetRandomString(),
            ServerPort = DataUtilityService.GetRandomInteger(),
            ServerSsl = true
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(email);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);
        
        // Act
        await service.AddUserEmail(user.Id, email.Id);
        var data = await databaseContext.UserEmails
            .Where(emails => emails.UserId == user.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().NotBeNull();
        data.UserId.Should().Be(user.Id);
        data.EmailId.Should().Be(email.Id);
    }

    [Fact]
    public async Task GivenIncorrectUserId_WhenAddUserEmail_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var email = new Emails
        {
            Address = DataUtilityService.GetRandomEmail(),
            IsActive = true,
            ServerName = DataUtilityService.GetRandomString(),
            ServerKey = DataUtilityService.GetRandomString(),
            ServerPort = DataUtilityService.GetRandomInteger(),
            ServerSsl = true
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(email);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);
        
        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.AddUserEmail(Guid.NewGuid(), email.Id));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXISTS));
    }

    [Fact]
    public async Task GivenIncorrectEmailId_WhenAddUserEmail_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var email = new Emails
        {
            Address = DataUtilityService.GetRandomEmail(),
            IsActive = true,
            ServerName = DataUtilityService.GetRandomString(),
            ServerKey = DataUtilityService.GetRandomString(),
            ServerPort = DataUtilityService.GetRandomInteger(),
            ServerSsl = true
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(email);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.AddUserEmail(user.Id, Guid.NewGuid()));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_ASSOCIATED_EMAIL));
    }

    [Fact]
    public async Task GivenIncorrectId_WhenUpdateUserEmail_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var emails = new List<Emails>
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Address = DataUtilityService.GetRandomEmail(),
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(),
                ServerSsl = true
            },
            new ()
            {
                Id = Guid.NewGuid(),
                Address = DataUtilityService.GetRandomEmail(),
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(),
                ServerSsl = true
            }
        };

        var userEmail = new UserEmails
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            EmailId = emails[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddRangeAsync(emails);
        await databaseContext.AddAsync(userEmail);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateUserEmail(Guid.NewGuid(),  emails[1].Id));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_ID));
    }

    [Fact]
    public async Task GivenExistingUserAndEmail_WhenUpdateUserEmail_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var emails = new List<Emails>
        {
            new ()
            {
                Id = Guid.NewGuid(),
                Address = DataUtilityService.GetRandomEmail(),
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(),
                ServerSsl = true
            },
            new ()
            {
                Id = Guid.NewGuid(),
                Address = DataUtilityService.GetRandomEmail(),
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(),
                ServerSsl = true
            }
        };

        var userEmail = new UserEmails
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            EmailId = emails[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddRangeAsync(emails);
        await databaseContext.AddAsync(userEmail);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        await service.UpdateUserEmail(userEmail.Id, emails[1].Id);
        var data = await databaseContext.UserEmails
            .Where(userEmails => userEmails.Id == userEmail.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().NotBeNull();
        data.EmailId.Should().Be(emails[1].Id);
    }

    [Fact]
    public async Task GivenIncorrectUserId_WhenRemoveUserEmail_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var email = new Emails
        {
            Id = Guid.NewGuid(),
            Address = DataUtilityService.GetRandomEmail(),
            IsActive = true,
            ServerName = DataUtilityService.GetRandomString(),
            ServerKey = DataUtilityService.GetRandomString(),
            ServerPort = DataUtilityService.GetRandomInteger(),
            ServerSsl = true
        };

        var userEmail = new UserEmails
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            EmailId = email.Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(email);
        await databaseContext.AddAsync(userEmail);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.RemoveUserEmail(Guid.NewGuid(), email.Id));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_ID_OR_EMAIL_ID_INVALID));
    }

    [Fact]
    public async Task GivenIncorrectEmailId_WhenRemoveUserEmail_ShouldThrowError()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var email = new Emails
        {
            Id = Guid.NewGuid(),
            Address = DataUtilityService.GetRandomEmail(),
            IsActive = true,
            ServerName = DataUtilityService.GetRandomString(),
            ServerKey = DataUtilityService.GetRandomString(),
            ServerPort = DataUtilityService.GetRandomInteger(),
            ServerSsl = true
        };

        var userEmail = new UserEmails
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            EmailId = email.Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(email);
        await databaseContext.AddAsync(userEmail);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.RemoveUserEmail(user.Id, Guid.NewGuid()));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_ID_OR_EMAIL_ID_INVALID));
    }

    [Fact]
    public async Task GivenExistingUserEmail_WhenRemoveUserEmail_ShouldSucceed()
    {
        // Arrange
        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            IsActivated = true,
            PrivateKey = DataUtilityService.GetRandomString()
        };

        var email = new Emails
        {
            Id = Guid.NewGuid(),
            Address = DataUtilityService.GetRandomEmail(),
            IsActive = true,
            ServerName = DataUtilityService.GetRandomString(),
            ServerKey = DataUtilityService.GetRandomString(),
            ServerPort = DataUtilityService.GetRandomInteger(),
            ServerSsl = true
        };

        var userEmail = new UserEmails
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            EmailId = email.Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(email);
        await databaseContext.AddAsync(userEmail);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Assert
        await service.RemoveUserEmail(user.Id, email.Id);
        var data = await databaseContext.UserEmails
            .Where(userEmails => userEmails.UserId == user.Id && userEmails.EmailId == email.Id)
            .FirstOrDefaultAsync();

        // Act
        data.Should().BeNull();
    }
}