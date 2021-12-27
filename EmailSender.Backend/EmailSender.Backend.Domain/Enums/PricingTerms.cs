namespace EmailSender.Backend.Domain.Enums;

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Core.Converters;

[JsonConverter(typeof(StringToEnumWithDefaultConverter))]
public enum PricingTerms
{
    [EnumMember(Value = "unknown")]
    Unknown = 0,

    [EnumMember(Value = "same day")]
    SameDay = 1,

    [EnumMember(Value = "next day")]
    NextDay = 2,

    [EnumMember(Value = "one week")]
    Week = 8,

    [EnumMember(Value = "two weeks")]
    TwoWeeks = 15,

    [EnumMember(Value = "one month")]
    OneMonth = 31,

    [EnumMember(Value = "two months")]
    TwoMonths = 61
}