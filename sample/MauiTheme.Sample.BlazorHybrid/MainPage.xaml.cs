using MauiTheme.Core;

namespace MauiTheme.Sample.BlazorHybrid;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    MauiAppTheme _currentAppTheme;
    public MauiAppTheme Selection
    {
        get {
            if(MauiTheme.Default.CurrentAppTheme != _currentAppTheme)
            {
                _currentAppTheme = MauiTheme.Default.CurrentAppTheme;
                OnPropertyChanged(nameof(Selection));
                return _currentAppTheme;
            }
            return _currentAppTheme;
        }
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
