namespace EmailSender.Backend.Tests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Shared.Resources;
    using EmailService.Requests;
    using EmailService.Validators;

    public class GetAllowEmailsRequestValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetAllowEmailsRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetAllowEmailsRequest
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetAllowEmailsRequestValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetAllowEmailsRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetAllowEmailsRequest
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetAllowEmailsRequestValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}