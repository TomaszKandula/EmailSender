namespace EmailSender.UnitTests.Validators;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Queries.Users;

public class GetUserEmailsQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenPrivateKey_WhenGetAllowEmailsRequest_ShouldSucceed()
    {
        // Arrange
        var request = new GetUserEmailsQuery
        {
            PrivateKey = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetUserEmailsQueryValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyPrivateKey_WhenGetAllowEmailsRequest_ShouldThrowError()
    {
        // Arrange
        var request = new GetUserEmailsQuery
        {
            PrivateKey = string.Empty
        };

        // Act
        var validator = new GetUserEmailsQueryValidator();
        var result = validator.Validate(request);

        // Assert
        result.Errors.Should().HaveCount(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}