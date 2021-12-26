namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Billings;

    public class GetAllUserBillingsQueryValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetAllUserBillingsRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetAllUserBillingsQuery
            {
                PrivateKey = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new GetAllUserBillingsQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetAllUserBillingsRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetAllUserBillingsQuery
            {
                PrivateKey = string.Empty
            };

            // Act
            var validator = new GetAllUserBillingsQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}