using MauiTheme.Core;

namespace MauiTheme.Helpers;
internal static class EnumHelper
{
    public static AppTheme MapMauiTheme(MauiAppTheme mauiAppTheme) =>
        mauiAppTheme switch
        {
            MauiAppTheme.Unspecified => AppTheme.Unspecified,
            MauiAppTheme.Dark => AppTheme.Dark,
            MauiAppTheme.Light => AppTheme.Light,
            _ => AppTheme.Unspecified,
        };
    
}
