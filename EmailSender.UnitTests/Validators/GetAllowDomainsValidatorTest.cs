namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Cqrs.Requests;
    using Backend.Cqrs.Validators;
    using Backend.Shared.Resources;

    public class GetAllowDomainsValidatorTest : TestBase
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
            var validator = new GetAllowDomainsValidator();
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
            var validator = new GetAllowDomainsValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}