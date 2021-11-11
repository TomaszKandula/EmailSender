namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using System;
    using Backend.Cqrs.Requests;
    using Backend.Cqrs.Validators;
    using Backend.Shared.Resources;

    public class GetUserBillingValidatorTest : TestBase
    {
        [Fact]
        public void GivenPrivateKey_WhenGetUserBillingRequest_ShouldSucceed()
        {
            // Arrange
            var request = new GetUserBillingRequest
            {
                PrivateKey = DataUtilityService.GetRandomString(),
                BillingId = Guid.NewGuid()
            };

            // Act
            var validator = new GetUserBillingValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyPrivateKey_WhenGetUserBillingRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetUserBillingRequest
            {
                PrivateKey = string.Empty,
                BillingId = Guid.NewGuid()
            };

            // Act
            var validator = new GetUserBillingValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmptyBillingId_WhenGetUserBillingRequest_ShouldThrowError()
        {
            // Arrange
            var request = new GetUserBillingRequest
            {
                PrivateKey = DataUtilityService.GetRandomString(),
                BillingId = Guid.Empty
            };

            // Act
            var validator = new GetUserBillingValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmptyPrivateKeyAndBillingId_WhenGetUserBillingRequest_ShouldThrowErrors()
        {
            // Arrange
            var request = new GetUserBillingRequest
            {
                PrivateKey = string.Empty,
                BillingId = Guid.Empty
            };

            // Act
            var validator = new GetUserBillingValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(2);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}