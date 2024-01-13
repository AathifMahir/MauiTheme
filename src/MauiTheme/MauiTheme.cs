using MauiTheme.Core;

namespace MauiTheme;
public static class MauiTheme
{
    public static MauiAppTheme CurrentAppTheme 
    {
        get => Default.CurrentAppTheme;
        set => Default.CurrentAppTheme = value;
    }
    public static string CurrentResource 
    {
        get => Default.CurrentResource;
        set => Default.CurrentResource = value;
    }

    public static void InitializeTheme<TApp>(Action<ThemeConfiguration> configs) where TApp : Application => Default.InitializeTheme<TApp>(configs);

    internal static void SetDefault(IMauiTheme? implementation) =>
       defaultTheme = implementation;

    static IMauiTheme? defaultTheme;

    public static IMauiTheme Default => defaultTheme ??= new MauiThemeDefault();
}
