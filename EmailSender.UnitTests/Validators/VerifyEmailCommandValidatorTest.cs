namespace EmailSender.UnitTests.Validators
{
    using Xunit;
    using FluentAssertions;
    using System.Collections.Generic;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Commands.Emails;

    public class VerifyEmailCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenValidInputs_WhenVerifyEmailRequest_ShouldSucceed()
        {
            // Arrange
            var request = new VerifyEmailCommand
            {
                PrivateKey = DataUtilityService.GetRandomString(),
                Emails = new List<string>
                {
                    DataUtilityService.GetRandomEmail(),
                    DataUtilityService.GetRandomEmail()
                }
            };

            // Act
            var validator = new VerifyEmailCommandValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenInvalidInputs_WhenVerifyEmailRequest_ShouldThrowErrors()
        {
            // Arrange
            var request = new VerifyEmailCommand
            {
                PrivateKey = string.Empty,
                Emails = null
            };

            // Act
            var validator = new VerifyEmailCommandValidator();
            var result = validator.Validate(request);

            // Assert
            result.Errors.Should().HaveCount(2);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}