namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Users;

    public class GetUserDetailsQueryValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetUserDetailsRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetUserDetailsQuery
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetUserDetailsQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetUserDetailsRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetUserDetailsQuery
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetUserDetailsQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}