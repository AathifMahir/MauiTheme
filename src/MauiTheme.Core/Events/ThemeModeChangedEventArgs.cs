namespace MauiTheme.Core.Events;
public sealed class ThemeModeChangedEventArgs : EventArgs
{
    public ThemeMode CurrentTheme { get; set; }

    public ThemeModeChangedEventArgs(ThemeMode currentTheme)
    {
        CurrentTheme = currentTheme;
    }
}
