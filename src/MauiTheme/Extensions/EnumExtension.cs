using MauiTheme.Core;

namespace MauiTheme.Extensions;
internal static class EnumExtension
{
    internal static AppTheme MapToAppTheme(this MauiAppTheme mauiAppTheme) =>
        mauiAppTheme switch
        {
            MauiAppTheme.Unspecified => AppTheme.Unspecified,
            MauiAppTheme.Dark => AppTheme.Dark,
            MauiAppTheme.Light => AppTheme.Light,
            _ => AppTheme.Unspecified,
        };
}
