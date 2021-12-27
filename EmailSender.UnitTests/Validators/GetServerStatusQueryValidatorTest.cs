namespace EmailSender.UnitTests.Validators;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Queries.Smtp;

public class GetServerStatusQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenPrivateKeyAndEmail_WhenGetServerStatusRequest_ShouldSucceed()
    {
        // Arrange
        var request = new GetServerStatusQuery
        {
            PrivateKey = DataUtilityService.GetRandomString(),
            EmailAddress = DataUtilityService.GetRandomEmail()
        };

        // Act
        var validator = new GetServerStatusQueryValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyPrivateKey_WhenGetServerStatusRequest_ShouldThrowError()
    {
        // Arrange
        var request = new GetServerStatusQuery
        {
            PrivateKey = string.Empty,
            EmailAddress = DataUtilityService.GetRandomEmail()
        };

        // Act
        var validator = new GetServerStatusQueryValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().HaveCount(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenEmptyEmail_WhenGetServerStatusRequest_ShouldThrowError()
    {
        // Arrange
        var request = new GetServerStatusQuery
        {
            PrivateKey = DataUtilityService.GetRandomString(),
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

    [Fact]
    public void GivenEmptyProperties_WhenGetServerStatusRequest_ShouldThrowError()
    {
        // Arrange
        var request = new GetServerStatusQuery
        {
            PrivateKey = string.Empty,
            EmailAddress = string.Empty
        };

        // Act
        var validator = new GetServerStatusQueryValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().HaveCount(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_EMAIL_ADDRESS));
    }
}