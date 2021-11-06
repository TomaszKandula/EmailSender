namespace EmailSender.Backend.Tests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Shared.Resources;
    using EmailService.Requests;
    using EmailService.Validators;

    public class GetAllowDomainsRequestValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetAllowDomainsRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetAllowDomainsRequest
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetAllowDomainsRequestValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetAllowDomainsRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetAllowDomainsRequest
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetAllowDomainsRequestValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}