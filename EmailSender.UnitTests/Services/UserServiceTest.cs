namespace EmailSender.UnitTests.Services
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Backend.UserService;
    using Backend.Domain.Entities;
    using Backend.Shared.Services.LoggerService;

    public class UserServiceTest : TestBase
    {
        [Fact]
        public async Task GivenDomainName_WhenInvokeIsDomainAllowed_ShouldSucceed()
        {
            // Arrange
            var domainName = DataUtilityService.GetRandomString(useAlphabetOnly: true);

            var user = new User
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
            
            var allowDomain = new AllowDomain
            {
                UserId = user.Id,
                Host = domainName
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.AddAsync(user);
            await databaseContext.AddAsync(allowDomain);
            await databaseContext.SaveChangesAsync();
            
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new UserService(
                databaseContext,
                mockedLoggerService.Object);

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

            var user = new User
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
            
            var allowDomain = new AllowDomain
            {
                UserId = user.Id,
                Host = DataUtilityService.GetRandomString(useAlphabetOnly: true)
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.AddAsync(user);
            await databaseContext.AddAsync(allowDomain);
            await databaseContext.SaveChangesAsync();
            
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new UserService(
                databaseContext, 
                mockedLoggerService.Object);

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
            var user = new User
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
            var service = new UserService(
                databaseContext, 
                mockedLoggerService.Object);

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
            var user = new User
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
            var service = new UserService(
                databaseContext, 
                mockedLoggerService.Object);

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
            var user = new User
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
            var service = new UserService(
                databaseContext, 
                mockedLoggerService.Object);

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
            var user = new User
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
            var service = new UserService(
                databaseContext, 
                mockedLoggerService.Object);

            // Act
            var result = await service.GetUserByPrivateKey(privateKey, CancellationToken.None);

            // Assert
            (result == Guid.Empty).Should().BeTrue();
        }
    }
}