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
    using EmailService.Services.SenderService;
    
    public class SenderServiceTest : TestBase
    {
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
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object);

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
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object);

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
            var service = new SenderService(
                databaseContext, 
                mockedSmtpClientService.Object);

            // Act
            var result = await service.VerifyEmailFrom(emailFrom, userId, CancellationToken.None);

            // Assert
            (result != Guid.Empty).Should().BeFalse();
        }
    }
}