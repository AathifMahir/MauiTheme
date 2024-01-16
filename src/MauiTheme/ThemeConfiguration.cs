using MauiTheme.Core;

namespace MauiTheme;
public sealed class ThemeConfiguration
{
    /// <summary>
    /// Represents the default theme to be applied when the user has not selected any theme.
    /// </summary>
    /// <remarks>
    /// Defaults to MauiAppTheme.Unspecified
    /// </remarks>
    public MauiAppTheme DefaultTheme { get; set; } = MauiAppTheme.Unspecified;

    /// <summary>
    /// Implies an array of style resources that should be applied consistently with every theme change.
    /// </summary>
    /// <remarks>
    /// Example: ["Resources/Styles/Styles.xaml"]
    /// </remarks>
    public string[] DefaultStyleResources { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Represents a dictionary of resources in the project, excluding the style resources.
    /// </summary>
    /// <remarks>
    /// Example: {"Blue", "Resources/Styles/Blue.xaml"}, {"Purple", "Resources/Styles/Colors.xaml"}
    /// </remarks>
    public Dictionary<string, string> Resources { get; set; } = new();
}

