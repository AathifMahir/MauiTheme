using System.ComponentModel;

namespace MauiTheme.Sample;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{



    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public AppTheme Selection 
    {
        get => Theme.Default.CurrentAppTheme;
        set
        {
            if(value != Theme.Default.CurrentAppTheme)
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

