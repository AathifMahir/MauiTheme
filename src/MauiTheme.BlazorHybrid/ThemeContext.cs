using MauiTheme.Core;

namespace MauiTheme.BlazorHybrid;
public sealed class ThemeContext
{
    private readonly IThemeHybrid _mauiTheme;
    public event Action? OnChanged;

    private ThemeContext(IThemeHybrid mauiTheme)
    {
        _mauiTheme = mauiTheme;
        _mauiTheme.ThemeChanged += MauiTheme_ThemeChanged;
        _mauiTheme.ResourceChanged += MauiTheme_ResourceChanged;
    }

    public static ThemeContext Create(IThemeHybrid mauiTheme, Action onThemeChangedCallback)
    {
        var theme = new ThemeContext(mauiTheme);
        theme.OnChanged += onThemeChangedCallback;
        return theme;
    }

    private void MauiTheme_ResourceChanged(object? sender, Core.Events.ResourceChangedEventArgs e)
    {
        OnChanged?.Invoke();
    }

    private void MauiTheme_ThemeChanged(object? sender, Core.Events.MauiAppThemeChangedEventArgs e)
    {
        OnChanged?.Invoke();
    }

    public void Dispose()
    {
        _mauiTheme.ThemeChanged -= MauiTheme_ThemeChanged;
        _mauiTheme.ResourceChanged -= MauiTheme_ResourceChanged;
    }
}
