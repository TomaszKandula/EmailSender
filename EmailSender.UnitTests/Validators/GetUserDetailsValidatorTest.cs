namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Shared.Resources;
    using Backend.AppService.Requests;
    using Backend.AppService.Validators;

    public class GetUserDetailsValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetUserDetailsRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetUserDetailsRequest
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetUserDetailsValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetUserDetailsRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetUserDetailsRequest
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetUserDetailsValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}