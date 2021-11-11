namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Cqrs.Requests;
    using Backend.Cqrs.Validators;
    using Backend.Shared.Resources;

    public class GetSentHistoryValidatorTest : TestBase
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
            var validator = new GetSentHistoryValidator();
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
            var validator = new GetSentHistoryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}