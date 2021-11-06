namespace EmailSender.Backend.Tests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Shared.Resources;
    using EmailService.Requests;

    public class GetUserDetailsRequestValidatorTest : TestBase
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
            var validator = new GetUserDetailsRequestValidator();
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
            var validator = new GetUserDetailsRequestValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}