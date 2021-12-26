namespace EmailSender.Backend.Domain.Enums
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Core.Converters;

    [JsonConverter(typeof(StringToEnumWithDefaultConverter))]
    public enum PaymentStatuses
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,

        [EnumMember(Value = "unpaid")]
        Unpaid = 1,

        [EnumMember(Value = "partially paid")]
        PartiallyPaid = 2,

        [EnumMember(Value = "paid")]
        Paid = 3
    }
}