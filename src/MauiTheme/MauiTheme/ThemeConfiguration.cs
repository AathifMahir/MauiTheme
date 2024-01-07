﻿namespace MauiTheme;
public sealed class ThemeConfiguration
{
    public AppTheme DefaultTheme { get; set; } = AppTheme.Unspecified;
    public string[] DefaultStyleResources { get; set; } = [];
    public Dictionary<string, string> Resources { get; set; } = [];
}
