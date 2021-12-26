namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Domains;

    public class GetAllowDomainsQueryValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetAllowDomainsRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetAllowDomainsQuery
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetAllowDomainsQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetAllowDomainsRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetAllowDomainsQuery
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetAllowDomainsQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}