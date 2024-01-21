using MauiTheme.Core;
using MauiTheme.Core.Events;

namespace MauiTheme.Sample.BlazorHybrid;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        Theme.Default.ThemeChanged += Default_ThemeChanged;
        Theme.Default.ResourceChanged += Default_ResourceChanged;
    }

    private void Default_ResourceChanged(object? sender, ResourceChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ColorKey));
    }

    private void Default_ThemeChanged(object? sender, ThemeModeChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Selection));
    }
    public ThemeMode Selection
    {
        get => Theme.Default.CurrentAppTheme;
        set
        {
            if (value != Theme.Default.CurrentAppTheme)
            {
                Theme.Default.CurrentAppTheme = value;
                OnPropertyChanged(nameof(Selection));
            }
        }
    }
    public string ColorKey
    {
        get => Theme.Default.CurrentResource;
        set
        {
            if (value != Theme.Default.CurrentResource)
            {
                Theme.Default.CurrentResource = value;
                OnPropertyChanged(nameof(ColorKey));
            }
        }
    }
}
