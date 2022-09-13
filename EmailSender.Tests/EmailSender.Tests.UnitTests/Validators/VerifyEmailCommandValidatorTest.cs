using Xunit;
using EmailSender.Backend.Application.Emails;
using EmailSender.Backend.Shared.Resources;
using FluentAssertions;

namespace EmailSender.Tests.UnitTests.Validators;

public class VerifyEmailCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenVerifyEmailRequest_ShouldSucceed()
    {
        // Arrange
        var request = new VerifyEmailCommand
        {
            Emails = new List<string>
            {
                DataUtilityService.GetRandomEmail(),
                DataUtilityService.GetRandomEmail()
            }
        };

        // Act
        var validator = new VerifyEmailCommandValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenInvalidInputs_WhenVerifyEmailRequest_ShouldThrowErrors()
    {
        // Arrange
        var request = new VerifyEmailCommand
        {
            Emails = new List<string>()
        };

        // Act
        var validator = new VerifyEmailCommandValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().HaveCount(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}