using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace EmailSender.Backend.Core.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class AccessException : Exception
{
    public string ErrorCode { get; } = "";

    protected AccessException(SerializationInfo serializationInfo, 
        StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    public AccessException(string errorCode, string errorMessage = "") : base(errorMessage)
        => ErrorCode = errorCode;
}