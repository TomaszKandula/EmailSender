namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using System;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Queries.Billings;

    public class GetUserBillingQueryValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetUserBillingRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetUserBillingQuery
            {
                PrivateKey = DataUtilityService.GetRandomString(),
                BillingId = Guid.NewGuid()
            };

            // Act
            var validator = new GetUserBillingQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetUserBillingRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetUserBillingQuery
            {
                PrivateKey = string.Empty,
                BillingId = Guid.NewGuid()
            };

            // Act
            var validator = new GetUserBillingQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmptyBillingId_WhenGetUserBillingRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetUserBillingQuery
            {
                PrivateKey = DataUtilityService.GetRandomString(),
                BillingId = Guid.Empty
            };

            // Act
            var validator = new GetUserBillingQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmptyPrivateKeyAndBillingId_WhenGetUserBillingRequest_ShouldThrowErrors()
        {
            // Arrange
            var request = new GetUserBillingQuery
            {
                PrivateKey = string.Empty,
                BillingId = Guid.Empty
            };

            // Act
            var validator = new GetUserBillingQueryValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(2);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}