namespace MauiTheme.Core.Events;
public sealed class MauiAppThemeChangedEventArgs : EventArgs
{
    public MauiAppTheme CurrentTheme { get; set; }

    public MauiAppThemeChangedEventArgs(MauiAppTheme currentTheme)
    {
        CurrentTheme = currentTheme;
    }
}
