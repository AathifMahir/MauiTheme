using MauiTheme.Core;

namespace MauiTheme.Extensions;
internal static class EnumExtension
{
    internal static AppTheme MapToAppTheme(this ThemeMode mauiAppTheme) =>
        mauiAppTheme switch
        {
            ThemeMode.Unspecified => AppTheme.Unspecified,
            ThemeMode.Dark => AppTheme.Dark,
            ThemeMode.Light => AppTheme.Light,
            _ => AppTheme.Unspecified,
        };
}
