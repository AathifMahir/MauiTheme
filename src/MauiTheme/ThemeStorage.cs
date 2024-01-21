using MauiTheme.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MauiTheme;
public sealed class ThemeStorage
{
    [JsonPropertyName("Theme")]
    public ThemeMode AppTheme { get; set; }

    [JsonPropertyName("Resource")]
    public string Resource {  get; set; } = string.Empty;

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}
