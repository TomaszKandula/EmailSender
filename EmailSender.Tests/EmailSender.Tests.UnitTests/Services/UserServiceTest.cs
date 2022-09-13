using Moq;
using Xunit;
using FluentAssertions;
using EmailSender.Backend.Core.Exceptions;
using EmailSender.Backend.Core.Utilities.DateTimeService;
using EmailSender.Backend.Core.Utilities.LoggerService;
using EmailSender.Backend.Domain.Entities;
using EmailSender.Backend.Domain.Enums;
using EmailSender.Backend.Shared.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EmailSender.Services.UserService;
using EmailSender.Services.UserService.Models;

namespace EmailSender.Tests.UnitTests.Services;

public class UserServiceTest : TestBase
{
    [Fact]
    public async Task GivenDomainName_WhenInvokeIsDomainAllowed_ShouldSucceed()
    {
        // Arrange
        var ipAddress = DataUtilityService.GetRandomIpAddress();

        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
        };
            
        var ips = new UserAllowedIps
        {
            UserId = user.Id,
            IpAddress = ipAddress.ToString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(ips);
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
        var result = await service.IsIpAddressAllowed(ipAddress, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task GivenIncorrectDomainName_WhenInvokeIsDomainAllowed_ShouldFail()
    {
        // Arrange
        var ipAddress = DataUtilityService.GetRandomIpAddress();

        var user = new Users
        {
            Id = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAlias = DataUtilityService.GetRandomString(5),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            Registered = DateTimeService.Now.AddDays(-120),
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
        };
            
        var ips = new UserAllowedIps
        {
            UserId = user.Id,
            IpAddress = DataUtilityService.GetRandomIpAddress().ToString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.AddAsync(ips);
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
        var result = await service.IsIpAddressAllowed(ipAddress, CancellationToken.None);

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
            Status = UserStatus.Activated,
            PrivateKey = privateKey,
            Role = UserRole.OrdinaryUser
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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
            Status = UserStatus.Activated,
            PrivateKey = privateKey,
            Role = UserRole.OrdinaryUser
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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
        var input = new AddUserInput
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
        var result = await service.AddUser(input);

        // Assert
        result.PrivateKey.Should().NotBeEmpty();
        result.PrivateKey.Should().HaveLength(32);
        result.EmailAddress.Should().Be(input.EmailAddress);
        result.UserAlias.Should().Be($"{input.FirstName[..2]}{input.LastName[..3]}");
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
            Status = UserStatus.Activated,
            IsDeleted  = false,
            Registered  = DateTimeService.Now,
            PrivateKey  = Guid.NewGuid().ToString("N"),
            Role = UserRole.OrdinaryUser
        };

        var input = new AddUserInput
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
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.AddUser(input));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_EMAIL_ALREADY_EXIST));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var input = new UpdateUserInput
        {
            UserId = user.Id,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            EmailAddress = DataUtilityService.GetRandomEmail()
        };

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        // Act
        await service.UpdateUser(input);
        var data = await databaseContext.Users
            .Where(users => users.Id == user.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().NotBeNull();
        data.FirstName.Should().Be(input.FirstName);
        data.LastName.Should().Be(input.LastName);
        data.EmailAddress.Should().Be(input.EmailAddress);
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
                Status = UserStatus.Activated,
                PrivateKey = DataUtilityService.GetRandomString(),
                Role = UserRole.OrdinaryUser
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                UserAlias = DataUtilityService.GetRandomString(5),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Registered = DateTimeService.Now.AddDays(-120),
                Status = UserStatus.Activated,
                PrivateKey = DataUtilityService.GetRandomString(),
                Role = UserRole.OrdinaryUser
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user[0].PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new UpdateUserInput
        {
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            EmailAddress = user[1].EmailAddress
        };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateUser(input));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_EMAIL_ALREADY_EXIST));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddRangeAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new UpdateUserInput
        {
            UserId = Guid.NewGuid(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            EmailAddress = DataUtilityService.GetRandomEmail()
        };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateUser(input));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_PRIVATE_KEY));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        var userId = Guid.NewGuid();
        var input = new RemoveUserInput { UserId = userId };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.RemoveUser(input));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_PRIVATE_KEY));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);
        
        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new RemoveUserInput { UserId = user.Id };

        // Act
        await service.RemoveUser(input);
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);
        
        var input = new RemoveUserInput { UserId = user.Id, SoftDelete = true };

        // Act
        await service.RemoveUser(input, CancellationToken.None);
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        var input = new UpdateUserDetailsInput
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
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateUserDetails(input));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_PRIVATE_KEY));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLoggerService = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedHttpContext = new Mock<IHttpContextAccessor>();

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new UpdateUserDetailsInput
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
        await service.UpdateUserDetails(input);
        var data = await databaseContext.UserDetails
            .Where(details => details.UserId == user.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().NotBeNull();
        data.CompanyName.Should().Be(input.CompanyName);
        data.VatNumber.Should().Be(input.VatNumber); 
        data.StreetAddress.Should().Be(input.StreetAddress); 
        data.PostalCode.Should().Be(input.PostalCode); 
        data.Country.Should().Be(input.Country); 
        data.City.Should().Be(input.City); 
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);
        
        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new UpdateUserDetailsInput
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
        await service.UpdateUserDetails(input);
        var data = await databaseContext.UserDetails
            .Where(details => details.UserId == user.Id)
            .FirstOrDefaultAsync();

        // Assert
        data.Should().NotBeNull();
        data.CompanyName.Should().Be(input.CompanyName);
        data.VatNumber.Should().Be(input.VatNumber); 
        data.StreetAddress.Should().Be(input.StreetAddress); 
        data.PostalCode.Should().Be(input.PostalCode); 
        data.Country.Should().Be(input.Country); 
        data.City.Should().Be(input.City); 
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new AddUserEmailInput
        {
            UserId = user.Id,
            EmailId = email.Id
        };

        // Act
        await service.AddUserEmail(input);
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new AddUserEmailInput
        {
            UserId = Guid.NewGuid(),
            EmailId = email.Id
        };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.AddUserEmail(input));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXIST));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new AddUserEmailInput
        {
            UserId = user.Id,
            EmailId = Guid.NewGuid()
        };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.AddUserEmail(input, CancellationToken.None));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
        };

        var emails = new List<Emails>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Address = DataUtilityService.GetRandomEmail(),
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(),
                ServerSsl = true
            },
            new()
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

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new UpdateUserEmailInput
        {
            UserId = user.Id,
            OldEmailId = Guid.NewGuid(),
            NewEmailId = emails[1].Id
        };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateUserEmail(input, CancellationToken.None));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new UpdateUserEmailInput
        {
            UserId = user.Id,
            OldEmailId = userEmail.Id,
            NewEmailId = emails[1].Id
        };

        // Act
        await service.UpdateUserEmail(input, CancellationToken.None);
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new RemoveUserEmailInput
        {
            UserId = Guid.NewGuid(),
            EmailId = email.Id
        };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.RemoveUserEmail(input, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXIST));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new RemoveUserEmailInput
        {
            UserId = user.Id,
            EmailId = Guid.NewGuid()
        };

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.RemoveUserEmail(input, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_EMAIL_ID));
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
            Status = UserStatus.Activated,
            PrivateKey = DataUtilityService.GetRandomString(),
            Role = UserRole.OrdinaryUser
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

        mockedHttpContext
            .Setup(accessor => accessor.HttpContext!.Request.Headers["X-Private-Key"])
            .Returns(user.PrivateKey);

        var service = new UserService(
            databaseContext, 
            mockedLoggerService.Object, 
            mockedHttpContext.Object, 
            mockedDateTimeService.Object);

        var input = new RemoveUserEmailInput
        {
            UserId = user.Id,
            EmailId = email.Id
        };

        // Assert
        await service.RemoveUserEmail(input, CancellationToken.None);
        var data = await databaseContext.UserEmails
            .Where(userEmails => userEmails.UserId == user.Id && userEmails.EmailId == email.Id)
            .FirstOrDefaultAsync();

        // Act
        data.Should().BeNull();
    }
}