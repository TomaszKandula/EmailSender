namespace EmailSender.UnitTests.Services
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using DnsClient;
    using MimeKit;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Backend.SmtpService;
    using Backend.SenderService;
    using Backend.Domain.Entities;
    using Backend.Core.Exceptions;
    using Backend.Shared.Resources;
    using Backend.SenderService.Models;
    
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
                ServerPort = DataUtilityService.GetRandomInteger(25, 459),
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
                ServerPort = DataUtilityService.GetRandomInteger(25, 459),
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
                ServerPort = DataUtilityService.GetRandomInteger(25, 459),
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

        [Theory]
        [InlineData("victoria@gmail.com")]
        [InlineData("ester@gmail.com")]
        [InlineData("tokan@dfds.com")]
        public async Task GivenValidEmails_WhenVerifyEmailAddress_ShouldSucceed(string emailAddress)
        {
            // Arrange
            var smtpClient = new SmtpClient();
            var lookupClient = new LookupClient();

            var smtpService = new SmtpClientService(smtpClient, lookupClient);
            var databaseContext = GetTestDatabaseContext();

            var service = new SenderService(databaseContext, smtpService);
            var email = new List<string> { emailAddress };

            // Act
            var result = (await service.VerifyEmailAddress(email, CancellationToken.None)).ToArray();

            // Assert
            result.Should().HaveCount(1);
            result[0].Address.Should().Be(emailAddress);
            result[0].IsDomainValid.Should().Be(true);
            result[0].IsFormatCorrect.Should().Be(true);
        }

        [Theory]
        [InlineData("victoria.gmail", false, false)]
        [InlineData("ester@gmail", true, false)]
        public async Task GivenValidEmails_WhenVerifyEmailAddress_ShouldFail(string emailAddress, bool isFormatValid, bool isDomainValid)
        {
            // Arrange
            var smtpClient = new SmtpClient();
            var lookupClient = new LookupClient();

            var smtpService = new SmtpClientService(smtpClient, lookupClient);
            var databaseContext = GetTestDatabaseContext();

            var service = new SenderService(databaseContext, smtpService);
            var email = new List<string> { emailAddress };

            // Act
            var result = (await service.VerifyEmailAddress(email, CancellationToken.None)).ToArray();

            // Assert
            result.Should().HaveCount(1);
            result[0].Address.Should().Be(emailAddress);
            result[0].IsDomainValid.Should().Be(isDomainValid);
            result[0].IsFormatCorrect.Should().Be(isFormatValid);
        }

        [Fact]
        public async Task GivenWorkingServer_WhenVerifyConnection_ShouldSucceed()
        {
            // Arrange
            var mockedSmtpClient = new Mock<ISmtpClient>();
            var mockedLookupClient = new Mock<ILookupClient>();

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        It.IsAny<bool>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsConnected).Returns(true);
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsAuthenticated).Returns(true);            
            
            var smtpService = new SmtpClientService(mockedSmtpClient.Object, mockedLookupClient.Object);

            var email = new Email
            {
                Address = DataUtilityService.GetRandomEmail(),
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(25, 459),
                ServerSsl = true
            };
            
            var databaseContext = GetTestDatabaseContext();
            await databaseContext.AddAsync(email);
            await databaseContext.SaveChangesAsync();
           
            var service = new SenderService(databaseContext, smtpService);
            
            // Act
            // Assert
            await service.VerifyConnection(email.Id, CancellationToken.None);
        }

        [Fact]
        public async Task GivenWorkingServerAndInvalidEmail_WhenVerifyConnection_ShouldThrowError()
        {
            // Arrange
            var mockedSmtpClient = new Mock<ISmtpClient>();
            var mockedLookupClient = new Mock<ILookupClient>();

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        It.IsAny<bool>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsConnected).Returns(true);
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsAuthenticated).Returns(true);            
            
            var smtpService = new SmtpClientService(mockedSmtpClient.Object, mockedLookupClient.Object);
            var databaseContext = GetTestDatabaseContext();
            var service = new SenderService(databaseContext, smtpService);
            
            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => service.VerifyConnection(Guid.NewGuid(), CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.MISSING_SERVER_DATA));
        }

        public enum TestCase
        {
            NoConnection,
            NoAuthorization
        }

        [Theory]
        [InlineData(false, true, TestCase.NoConnection)]
        [InlineData(true, false, TestCase.NoAuthorization)]
        public async Task GivenNoConnectionOrNoAuthorization_WhenVerifyConnection_ShouldThrowError(bool isConnected, bool isAuthenticated, TestCase testCase)
        {
            // Arrange
            var mockedSmtpClient = new Mock<ISmtpClient>();
            var mockedLookupClient = new Mock<ILookupClient>();

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        It.IsAny<bool>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsConnected).Returns(isConnected);
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsAuthenticated).Returns(isAuthenticated);
            
            var smtpService = new SmtpClientService(mockedSmtpClient.Object, mockedLookupClient.Object);

            var email = new Email
            {
                Address = DataUtilityService.GetRandomEmail(),
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(25, 459),
                ServerSsl = true
            };
            
            var databaseContext = GetTestDatabaseContext();
            await databaseContext.AddAsync(email);
            await databaseContext.SaveChangesAsync();
           
            var service = new SenderService(databaseContext, smtpService);

            // Act
            // Assert
            switch (testCase)
            {
                case TestCase.NoConnection:
                    var noConnectionResult = await Assert.ThrowsAsync<BusinessException>(() 
                        => service.VerifyConnection(email.Id, CancellationToken.None));
                    noConnectionResult.ErrorCode.Should().Be(nameof(ErrorCodes.SMTP_NOT_CONNECTED));
                    break;
                case TestCase.NoAuthorization:
                    var noAuthorizationResult = await Assert.ThrowsAsync<BusinessException>(() 
                        => service.VerifyConnection(email.Id, CancellationToken.None));
                    noAuthorizationResult.ErrorCode.Should().Be(nameof(ErrorCodes.SMTP_NOT_AUTHENTICATED));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(testCase), testCase, null);
            }
        }

        [Fact]
        public async Task GivenValidEmailDataAndServerData_WhenInvokeSend_ShouldSucceed()
        {
            // Arrange
            var mockedSmtpClient = new Mock<ISmtpClient>();
            var mockedLookupClient = new Mock<ILookupClient>();

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .SendAsync(
                        It.IsAny<MimeMessage>(),
                        It.IsAny<CancellationToken>(), 
                        null))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        It.IsAny<bool>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsConnected).Returns(true);
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsAuthenticated).Returns(true);

            var smtpService = new SmtpClientService(mockedSmtpClient.Object, mockedLookupClient.Object);
            var email = new Email
            {
                Address = DataUtilityService.GetRandomEmail(),
                IsActive = true,
                ServerName = DataUtilityService.GetRandomString(),
                ServerKey = DataUtilityService.GetRandomString(),
                ServerPort = DataUtilityService.GetRandomInteger(25, 459),
                ServerSsl = true
            };
            
            var databaseContext = GetTestDatabaseContext();
            await databaseContext.AddAsync(email);
            await databaseContext.SaveChangesAsync();
           
            var service = new SenderService(databaseContext, smtpService);
            var configuration = new Configuration
            {
                From = email.Address,
                To = new List<string> { DataUtilityService.GetRandomEmail() },
                Cc = new List<string> { DataUtilityService.GetRandomEmail() },
                Bcc = new List<string> { DataUtilityService.GetRandomEmail() },
                Subject = DataUtilityService.GetRandomString(),
                Body = DataUtilityService.GetRandomString(),
                IsHtml = false
            };

            // Act
            // Assert
            await service.Send(configuration, CancellationToken.None);
        }

        [Fact]
        public async Task GivenMissingServerData_WhenInvokeSend_ShouldFail()
        {
            // Arrange
            var mockedSmtpClient = new Mock<ISmtpClient>();
            var mockedLookupClient = new Mock<ILookupClient>();

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .ConnectAsync(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<SecureSocketOptions>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .AuthenticateAsync(
                        It.IsAny<string>(), 
                        It.IsAny<string>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .SendAsync(
                        It.IsAny<MimeMessage>(),
                        It.IsAny<CancellationToken>(), 
                        null))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient
                .Setup(smtpClient => smtpClient
                    .DisconnectAsync(
                        It.IsAny<bool>(), 
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsConnected).Returns(true);
            mockedSmtpClient.SetupGet(smtpClient => smtpClient.IsAuthenticated).Returns(true);

            var smtpService = new SmtpClientService(mockedSmtpClient.Object, mockedLookupClient.Object);
            var databaseContext = GetTestDatabaseContext();
            var service = new SenderService(databaseContext, smtpService);
            var configuration = new Configuration
            {
                From = DataUtilityService.GetRandomEmail(),
                To = new List<string> { DataUtilityService.GetRandomEmail() },
                Cc = new List<string> { DataUtilityService.GetRandomEmail() },
                Bcc = new List<string> { DataUtilityService.GetRandomEmail() },
                Subject = DataUtilityService.GetRandomString(),
                Body = DataUtilityService.GetRandomString(),
                IsHtml = false
            };

            // Act
            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => service.Send(configuration, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.MISSING_SERVER_DATA));
        }
    }
}