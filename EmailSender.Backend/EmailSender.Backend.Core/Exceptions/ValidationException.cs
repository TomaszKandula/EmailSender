using System;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;

namespace EmailSender.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class ValidationException : BusinessException
{
    public ValidationResult ValidationResult { get; }

    protected ValidationException(SerializationInfo serializationInfo, 
        StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    public ValidationException(ValidationResult validationResult, string errorMessage = "") : base(errorMessage)
        => ValidationResult = validationResult;
}