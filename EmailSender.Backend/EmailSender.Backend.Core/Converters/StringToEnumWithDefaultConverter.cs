using System.Reflection;
using System.Runtime.Serialization;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace EmailSender.Backend.Core.Converters;

/// <summary>
/// Dynamic String to Enum converter for unknown types. Converter sets Unknown member for unsupported types.
/// </summary>
/// <example>Add Unknown member to your Enum and add this attribute to model property.
/// <code>
/// [JsonProperty(ItemConverterType = typeof(StringToEnumWithDefaultConverter))]
/// public EnumType Type { get; set; }
/// </code>
/// </example>
[ExcludeFromCodeCoverage]
public class StringToEnumWithDefaultConverter : JsonConverter
{
    private ConcurrentDictionary<Type, ConcurrentDictionary<string, object>> _fromValueMap;
        
    private ConcurrentDictionary<Type, ConcurrentDictionary<object, string>> _toValueMap;

    private const string UnknownValue = "Unknown";

    private const string ValueInt32 = "value__";

    public override bool CanConvert(Type objectType)
    {
        var type = IsNullableType(objectType) 
            ? Nullable.GetUnderlyingType(objectType) 
            : objectType;

        return type != null && type.GetTypeInfo().IsEnum;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var isNullable = IsNullableType(objectType);
        var enumType = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

        InitMap(enumType);

        switch (reader.TokenType)
        {
            case JsonToken.String:
                var value = GetStringValue(reader, enumType);
                if (value != null)
                    return value;
                break;

            case JsonToken.Integer:
                ParseIntValue(reader, enumType, out var enumVal, out var values);
                if (values.Contains(enumVal))
                    return Enum.Parse(enumType!, enumVal.ToString());
                break;
        }

        if (isNullable) 
            return null;
            
        var unknownName = ParseNonNullable(enumType);

        if (unknownName == null)
            throw new JsonSerializationException($"Unable to parse '{reader.Value}' to enum {enumType}. Consider adding Unknown as fail-back value.");

        return Enum.Parse(enumType, unknownName);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var enumType = value.GetType();
        InitMap(enumType);
        var newValue = ToValue(enumType, value);
        writer.WriteValue(newValue);
    }

    private object GetStringValue(JsonReader reader, Type enumType)
    {
        var enumText = reader.Value.ToString();
        return FromValue(enumType, enumText);
    }

    private void InitMap(Type enumType)
    {
        _fromValueMap ??= new ConcurrentDictionary<Type, ConcurrentDictionary<string, object>>();
        _toValueMap ??= new ConcurrentDictionary<Type, ConcurrentDictionary<object, string>>();

        if (_fromValueMap.ContainsKey(enumType))
            return;

        var fromMap = new ConcurrentDictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
        var toMap = new ConcurrentDictionary<object, string>();

        var fields = enumType.GetRuntimeFields();

        var fieldInfos = fields.ToList();
        var hasUnknown = fieldInfos.FirstOrDefault(info => info.Name == UnknownValue);

        if (hasUnknown == null)
            throw new JsonSerializationException(
                $"Unable to parse to enum {enumType}. Because {enumType} do not have {UnknownValue} member");

        foreach (var field in fieldInfos)
        {
            var name = field.Name;
            object enumValue;

            try
            {
                enumValue = Enum.Parse(enumType, name == ValueInt32 ? UnknownValue : name);
            }
            catch (Exception)
            {
                enumValue = Enum.Parse(enumType, UnknownValue);
            }

            var enumMemberAttribute = field.GetCustomAttribute<EnumMemberAttribute>();
            if (enumMemberAttribute != null)
            {
                var enumMemberValue = enumMemberAttribute.Value ?? string.Empty;
                fromMap[enumMemberValue] = enumValue;
                toMap[enumValue] = enumMemberValue;
            }
            else
            {
                toMap[enumValue] = name;
            }

            fromMap[name] = enumValue;
        }

        _fromValueMap[enumType] = fromMap;
        _toValueMap[enumType] = toMap;
    }

    private string ToValue(Type enumType, object @object)
    {
        var map = _toValueMap[enumType];
        return map[@object];
    }

    private object FromValue(Type enumType, string value)
    {
        var map = _fromValueMap[enumType];
        return !map.ContainsKey(value) 
            ? null 
            : map[value];
    }

    private static void ParseIntValue(JsonReader reader, Type enumType, out int enumVal, out int[] values)
    {
        enumVal = Convert.ToInt32(reader.Value);
        values = (int[])Enum.GetValues(enumType);
    }

    private static string ParseNonNullable(Type enumType)
    {
        var names = Enum.GetNames(enumType);
        return names.FirstOrDefault(@string => string.Equals(@string, UnknownValue, StringComparison.OrdinalIgnoreCase));
    }

    private static bool IsNullableType(Type type) 
        => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
}