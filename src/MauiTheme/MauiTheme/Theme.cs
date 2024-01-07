namespace MauiTheme;
public static class Theme
{
    public static AppTheme CurrentAppTheme 
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

    internal static void SetDefault(ITheme? implementation) =>
       defaultTheme = implementation;

    static ITheme? defaultTheme;

    public static ITheme Default => defaultTheme ??= new ThemeDefault();
}
