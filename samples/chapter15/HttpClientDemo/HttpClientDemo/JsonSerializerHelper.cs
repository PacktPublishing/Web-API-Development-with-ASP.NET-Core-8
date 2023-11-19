using System.Text.Json;

namespace HttpClientDemo;

public static class JsonSerializerHelper
{
    public static string SerializeWithCamelCase<T>(T value)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        };
        return JsonSerializer.Serialize(value, options);
    }

    public static T? DeserializeWithCamelCase<T>(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        };
        return JsonSerializer.Deserialize<T>(json, options);
    }
}
