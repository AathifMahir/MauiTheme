using MauiTheme.Core;

namespace MauiTheme.BlazorHybrid;
public sealed class MauiThemeContext
{
    private readonly IMauiThemeHybrid _mauiTheme;
    public event Action? OnChanged;

    private MauiThemeContext(IMauiThemeHybrid mauiTheme)
    {
        _mauiTheme = mauiTheme;
        _mauiTheme.ThemeChanged += MauiTheme_ThemeChanged;
        _mauiTheme.ResourceChanged += MauiTheme_ResourceChanged;
    }

    public static MauiThemeContext Create(IMauiThemeHybrid mauiTheme, Action onThemeChangedCallback)
    {
        var theme = new MauiThemeContext(mauiTheme);
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
