namespace EmailSenderService.Backend.Shared.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    [Serializable]
    public class ServerException : Exception
    {
        public string ErrorCode { get; }

        protected ServerException(SerializationInfo serializationInfo, 
            StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

        public ServerException(string errorCode, string errorMessage = "") : base(errorMessage)
            => ErrorCode = errorCode;
    }
}