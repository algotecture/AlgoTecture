using System.Text.Json;

namespace AlgoTecture.Utils;

public static class JsonExtensions
{
    public static bool IsEmptyJson(this string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return true;

        try
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.ValueKind switch
            {
                JsonValueKind.Object => !doc.RootElement.EnumerateObject().Any(), // {}
                JsonValueKind.Array  => !doc.RootElement.EnumerateArray().Any(),  // []
                JsonValueKind.Null   => true,
                JsonValueKind.Undefined => true,
                _ => false
            };
        }
        catch (JsonException)
        {
            return true;
        }
    } 
}