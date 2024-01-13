namespace MauiTheme.Core;
public sealed class ThemeConfiguration
{
    public MauiAppTheme DefaultTheme { get; set; } = MauiAppTheme.Unspecified;
    public string[] DefaultStyleResources { get; set; } = Array.Empty<string>();
    public Dictionary<string, string> Resources { get; set; } = new();
}
