using MauiTheme.Core;
using MauiTheme.Core.Events;
using MauiTheme.Core.Exceptions;

namespace MauiTheme.BlazorHybrid;
internal sealed class ThemeHybridDefault : IThemeHybrid
{
    readonly ThemeMode _currentAppTheme = ThemeMode.Unspecified;
    public ThemeMode CurrentAppTheme 
    {
        get => _currentAppTheme;
        set
        {
            if(value == _currentAppTheme) return;
            if (_suppressException) 
            {
                Console.WriteLine("Theme Change is Not Supported in Blazor, Only Supported on Blazor Hybrid Which Runs on Native Invocation");
                return;
            }
            throw new MauiThemeException("Theme Change is Not Supported in Blazor, Only Supported on Blazor Hybrid Which Runs on Native Invocation");
        }
    }

    readonly string _currentRersource = string.Empty;
    public string CurrentResource 
    {
        get => _currentRersource;
        set
        {
            if(value == _currentRersource) return;
            if (_suppressException) 
            {
                Console.WriteLine("Resource Change is Not Supported in Blazor, Only Supported on Blazor Hybrid Which Runs on Native Invocation");
                return;
            }
            throw new MauiThemeException("Resource Change is Not Supported in Blazor, Only Supported on Blazor Hybrid Which Runs on Native Invocation");
        }
    }

    public ThemeContext Context { get; } = default!;

    readonly bool _suppressException;

    public event EventHandler<ThemeModeChangedEventArgs>? ThemeChanged;
    public event EventHandler<ResourceChangedEventArgs>? ResourceChanged;

    public ThemeHybridDefault(bool suppressException)
    {
        _suppressException = suppressException;
    }
}
