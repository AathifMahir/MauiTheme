using MauiTheme.Core;
using MauiTheme.Core.Events;

namespace MauiTheme.Sample.BlazorHybrid;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        MauiTheme.Default.ThemeChanged += Default_ThemeChanged;
        MauiTheme.Default.ResourceChanged += Default_ResourceChanged;
    }

    private void Default_ResourceChanged(object? sender, ResourceChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ColorKey));
    }

    private void Default_ThemeChanged(object? sender, MauiAppThemeChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Selection));
    }
    public MauiAppTheme Selection
    {
        get => MauiTheme.Default.CurrentAppTheme;
        set
        {
            if (value != MauiTheme.Default.CurrentAppTheme)
            {
                MauiTheme.Default.CurrentAppTheme = value;
                OnPropertyChanged(nameof(Selection));
            }
        }
    }
    public string ColorKey
    {
        get => MauiTheme.Default.CurrentResource;
        set
        {
            if (value != MauiTheme.Default.CurrentResource)
            {
                MauiTheme.Default.CurrentResource = value;
                OnPropertyChanged(nameof(ColorKey));
            }
        }
    }
}
