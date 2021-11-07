namespace EmailSender.Backend.Tests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Shared.Resources;
    using EmailService.Requests;
    using EmailService.Validators;

    public class GetAllUserBillingsValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetAllUserBillingsRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetAllUserBillingsRequest
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetAllUserBillingsValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetAllUserBillingsRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetAllUserBillingsRequest
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetAllUserBillingsValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}