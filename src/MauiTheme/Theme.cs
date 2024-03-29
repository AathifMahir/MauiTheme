﻿using MauiTheme.Core;
using MauiTheme.Core.Events;
using System.Windows.Input;

namespace MauiTheme;
public static class Theme
{
    public static ThemeMode CurrentAppTheme 
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

    public static event EventHandler<ThemeModeChangedEventArgs>? ThemeChanged
    {
        add => Default.ThemeChanged += value;
        remove => Default.ThemeChanged -= value;
    }

    public static event EventHandler<ResourceChangedEventArgs>? ResourceChanged
    {
        add => Default.ResourceChanged += value;
        remove => Default.ResourceChanged -= value;
    }

    public static ICommand? ThemeChangedCommand
    {
        get => Default.ThemeChangedCommand;
        set => Default.ThemeChangedCommand = value;
    }

    public static ICommand? ResourceChangedCommand
    {
        get => Default.ResourceChangedCommand;
        set => Default.ResourceChangedCommand = value;
    }

    internal static void SetDefault(ITheme? implementation) =>
       defaultTheme = implementation;

    static ITheme? defaultTheme;

    public static ITheme Default => defaultTheme ??= new ThemeDefault();
}
