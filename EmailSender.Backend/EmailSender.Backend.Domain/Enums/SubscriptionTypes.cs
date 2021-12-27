namespace EmailSender.Backend.Domain.Enums;

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Core.Converters;

[JsonConverter(typeof(StringToEnumWithDefaultConverter))]
public enum SubscriptionTypes
{
    [EnumMember(Value = "unknown")]
    Unknown = 0,

    [EnumMember(Value = "payg")]
    PayAsYouGo = 1,

    [EnumMember(Value = "quarterly")]
    Quarterly = 2,

    [EnumMember(Value = "monthly")]
    Monthly = 3,

    [EnumMember(Value = "yearly")]
    Yearly = 4
}