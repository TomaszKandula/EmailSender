namespace EmailSender.Backend.Tests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Shared.Resources;
    using EmailService.Requests;

    public class GetSentHistoryRequestValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetSentHistoryRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetSentHistoryRequest
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetSentHistoryRequestValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetSentHistoryRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetSentHistoryRequest
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetSentHistoryRequestValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}