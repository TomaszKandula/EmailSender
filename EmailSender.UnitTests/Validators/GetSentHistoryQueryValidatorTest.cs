namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Cqrs.Requests;
    using Backend.Cqrs.Validators;
    using Backend.Shared.Resources;

    public class GetSentHistoryQueryValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetSentHistoryRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetSentHistoryQueryRequest
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetSentHistoryQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetSentHistoryRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetSentHistoryQueryRequest
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetSentHistoryQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}