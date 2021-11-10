namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Shared.Resources;
    using Backend.AppService.Requests;
    using Backend.AppService.Validators;

    public class GetAllowEmailsValidatorTest : TestBase
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
            var validator = new GetAllowEmailsValidator();
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
            var validator = new GetAllowEmailsValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}