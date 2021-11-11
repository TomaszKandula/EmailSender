namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Cqrs.Requests;
    using Backend.Cqrs.Validators;
    using Backend.Shared.Resources;

    public class GetAllowEmailsQueryValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetAllowEmailsRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetAllowEmailsQueryRequest
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetAllowEmailsQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetAllowEmailsRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetAllowEmailsQueryRequest
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetAllowEmailsQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}