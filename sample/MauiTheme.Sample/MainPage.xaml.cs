using MauiTheme.Core;
using System.ComponentModel;

namespace MauiTheme.Sample;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{



    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public MauiAppTheme Selection 
    {
        get => MauiTheme.Default.CurrentAppTheme;
        set
        {
            if(value != MauiTheme.Default.CurrentAppTheme)
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

