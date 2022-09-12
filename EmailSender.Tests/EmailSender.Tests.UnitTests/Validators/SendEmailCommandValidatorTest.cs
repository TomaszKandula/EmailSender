using Xunit;
using System.Collections.Generic;
using EmailSender.Backend.Application.Handlers.Commands.Emails;
using EmailSender.Backend.Shared.Resources;
using FluentAssertions;

namespace EmailSender.Tests.UnitTests.Validators;

public class SendEmailCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputsWithOptionalEmails_WhenInvokeSendEmailRequest_ShouldSucceed()
    {
        // Arrange
        var request = new SendEmailCommand
        {
            From = DataUtilityService.GetRandomEmail(),
            Subject = DataUtilityService.GetRandomString(),
            To = new List<string>
            {
                DataUtilityService.GetRandomEmail()
            },
            Cc = new List<string>
            {
                DataUtilityService.GetRandomEmail(),
                DataUtilityService.GetRandomEmail()
            },
            Bcc = new List<string>
            {
                DataUtilityService.GetRandomEmail(),
                DataUtilityService.GetRandomEmail()
            }
        };

        // Act
        var validator = new SendEmailCommandValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenValidInputsWithoutOptionalEmails_WhenInvokeSendEmailRequest_ShouldSucceed()
    {
        // Arrange
        var request = new SendEmailCommand
        {
            From = DataUtilityService.GetRandomEmail(),
            Subject = DataUtilityService.GetRandomString(),
            To = new List<string>
            {
                DataUtilityService.GetRandomEmail()
            },
            Cc = null,
            Bcc = null
        };

        // Act
        var validator = new SendEmailCommandValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenRequiredInputsEmptyWithoutOptionalEmails_WhenInvokeSendEmailRequest_ShouldThrowErrors()
    {
        // Arrange
        var request = new SendEmailCommand
        {
            From = string.Empty,
            Subject = string.Empty,
            To = new List<string> { string.Empty },
            Cc = null,
            Bcc = null
        };

        // Act
        var validator = new SendEmailCommandValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().HaveCount(5);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS));
    }

    [Fact]
    public void GivenRequiredInputsWithInvalidOptionalEmails_WhenInvokeSendEmailRequest_ShouldThrowErrors()
    {
        // Arrange
        var request = new SendEmailCommand
        {
            From = DataUtilityService.GetRandomEmail(),
            Subject = DataUtilityService.GetRandomString(),
            To = new List<string>
            {
                DataUtilityService.GetRandomEmail()
            },
            Cc = new List<string> { string.Empty },
            Bcc = new List<string> { string.Empty }
        };

        // Act
        var validator = new SendEmailCommandValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().HaveCount(4);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS));
    }
}