namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Emails;

    public class GetEmailsHistoryQueryValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetSentHistoryRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetEmailsHistoryQuery
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetEmailsHistoryQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetSentHistoryRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetEmailsHistoryQuery
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetEmailsHistoryQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}