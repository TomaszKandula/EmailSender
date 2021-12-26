namespace EmailSender.Backend.Domain.Enums
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Core.Converters;

    [JsonConverter(typeof(StringToEnumWithDefaultConverter))]
    public enum PricingStatuses
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,

        [EnumMember(Value = "inactive")]
        Inactive = 1,

        [EnumMember(Value = "active")]
        Active = 2
    }
}