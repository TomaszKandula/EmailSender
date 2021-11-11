namespace EmailSender.Backend.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    [Serializable]
    public class ProcessException : Exception
    {
        public string ErrorCode { get; }

        protected ProcessException(SerializationInfo serializationInfo, 
            StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

        public ProcessException(string errorCode, string errorMessage = "") : base(errorMessage)
            => ErrorCode = errorCode;
    }
}