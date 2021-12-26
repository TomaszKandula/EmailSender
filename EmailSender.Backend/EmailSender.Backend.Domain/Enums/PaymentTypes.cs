namespace EmailSender.Backend.Domain.Enums
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Core.Converters;

    [JsonConverter(typeof(StringToEnumWithDefaultConverter))]
    public enum PaymentTypes
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,

        [EnumMember(Value = "credit card")]
        CreditCard = 1,

        [EnumMember(Value = "wire transfer")]
        WireTransfer = 2
    }
}