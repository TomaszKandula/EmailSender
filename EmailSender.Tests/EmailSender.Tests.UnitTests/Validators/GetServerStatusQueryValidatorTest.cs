using EmailSender.Backend.Application.Smtp;
using EmailSender.Backend.Shared.Resources;
using FluentAssertions;
using Xunit;

namespace EmailSender.Tests.UnitTests.Validators;

public class GetServerStatusQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenPrivateKeyAndEmail_WhenGetServerStatusRequest_ShouldSucceed()
    {
        // Arrange
        var request = new GetServerStatusQuery
        {
            EmailAddress = DataUtilityService.GetRandomEmail()
        };

        // Act
        var validator = new GetServerStatusQueryValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyEmail_WhenGetServerStatusRequest_ShouldThrowError()
    {
        // Arrange
        var request = new GetServerStatusQuery
        {
            EmailAddress = string.Empty
        };

        // Act
        var validator = new GetServerStatusQueryValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().HaveCount(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS));
    }
}