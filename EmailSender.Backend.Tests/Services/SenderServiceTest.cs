namespace EmailSender.Backend.Tests.Services
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using SmtpService;
    using Domain.Entities;
    using EmailService.Services;
    using Shared.Services.LoggerService;
    
    public class SenderServiceTest : TestBase
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
            
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object, 
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
            
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object, 
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
            
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object, 
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
            
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object, 
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
            
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object, 
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
            
            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object, 
                mockedLoggerService.Object);

            // Act
            var result = await service.GetUserByPrivateKey(privateKey, CancellationToken.None);

            // Assert
            (result == Guid.Empty).Should().BeTrue();
        }

        [Fact]
        public async Task GivenActiveEmail_WhenVerifyEmailFrom_ShouldSucceed()
        {
            // Arrange
            var emailFrom = DataUtilityService.GetRandomEmail();
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                UserAlias = DataUtilityService.GetRandomString(5),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Registered = DateTimeService.Now.AddDays(-120),
                IsActivated = true,
                PrivateKey = DataUtilityService.GetRandomString()
            };

            var emailId = Guid.NewGuid();
            var email = new Email
            {
                Id = emailId,
                Address = emailFrom,
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(),
                ServerSsl = true
            };

            var allowEmail = new AllowEmail
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                EmailId = emailId
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.AddAsync(user);
            await databaseContext.AddAsync(email);
            await databaseContext.AddAsync(allowEmail);            
            await databaseContext.SaveChangesAsync();

            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object, 
                mockedLoggerService.Object);

            // Act
            var result = await service.VerifyEmailFrom(emailFrom, userId, CancellationToken.None);

            // Assert
            (result != Guid.Empty).Should().BeTrue();
        }

        [Fact]
        public async Task GivenInactiveEmailFrom_WhenVerifyEmailFrom_ShouldFail()
        {
            // Arrange
            var emailFrom = DataUtilityService.GetRandomEmail();
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                UserAlias = DataUtilityService.GetRandomString(5),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Registered = DateTimeService.Now.AddDays(-120),
                IsActivated = true,
                PrivateKey = DataUtilityService.GetRandomString()
            };

            var emailId = Guid.NewGuid();
            var email = new Email
            {
                Id = emailId,
                Address = emailFrom,
                IsActive = false,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(),
                ServerSsl = true
            };

            var allowEmail = new AllowEmail
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                EmailId = emailId
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.AddAsync(user);
            await databaseContext.AddAsync(email);
            await databaseContext.AddAsync(allowEmail);            
            await databaseContext.SaveChangesAsync();

            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object, 
                mockedLoggerService.Object);

            // Act
            var result = await service.VerifyEmailFrom(emailFrom, userId, CancellationToken.None);

            // Assert
            (result != Guid.Empty).Should().BeFalse();
        }

        [Fact]
        public async Task GivenNonExistingEmail_WhenVerifyEmailFrom_ShouldFail()
        {
            // Arrange
            var emailFrom = DataUtilityService.GetRandomEmail();
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                UserAlias = DataUtilityService.GetRandomString(5),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Registered = DateTimeService.Now.AddDays(-120),
                IsActivated = true,
                PrivateKey = DataUtilityService.GetRandomString()
            };

            var emailId = Guid.NewGuid();
            var email = new Email
            {
                Id = emailId,
                Address = DataUtilityService.GetRandomEmail(),
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(),
                ServerSsl = true
            };

            var allowEmail = new AllowEmail
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                EmailId = emailId
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.AddAsync(user);
            await databaseContext.AddAsync(email);
            await databaseContext.AddAsync(allowEmail);            
            await databaseContext.SaveChangesAsync();

            var mockedSmtpClientService = new Mock<ISmtpClientService>();
            var mockedLoggerService = new Mock<ILoggerService>();
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object, 
                mockedLoggerService.Object);

            // Act
            var result = await service.VerifyEmailFrom(emailFrom, userId, CancellationToken.None);

            // Assert
            (result != Guid.Empty).Should().BeFalse();
        }
    }
}