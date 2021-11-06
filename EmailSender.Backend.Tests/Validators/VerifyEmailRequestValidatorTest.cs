namespace EmailSender.Backend.Tests.Validators
{
    using Xunit;
    using FluentAssertions;
    using System.Collections.Generic;
    using Shared.Resources;
    using EmailService.Requests;
    using EmailService.Validators;

    public class VerifyEmailRequestValidatorTest : TestBase
    {
        [Fact]
        public void GivenValidInputs_WhenVerifyEmailRequest_ShouldSucceed()
        {
            // Arrange
            var request = new VerifyEmailRequest
            {
                PrivateKey = DataUtilityService.GetRandomString(),
                Emails = new List<string>
                {
                    DataUtilityService.GetRandomEmail(),
                    DataUtilityService.GetRandomEmail()
                }
            };

            // Act
            var validator = new VerifyEmailRequestValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenInvalidInputs_WhenVerifyEmailRequest_ShouldThrowErrors()
        {
            // Arrange
            var request = new VerifyEmailRequest
            {
                PrivateKey = string.Empty,
                Emails = null
            };

            // Act
            var validator = new VerifyEmailRequestValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(2);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}