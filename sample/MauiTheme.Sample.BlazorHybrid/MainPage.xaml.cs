using MauiTheme.Core;

namespace MauiTheme.Sample.BlazorHybrid;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }


    public static MauiAppTheme CurrentTheme 
    {
        get => MauiTheme.CurrentAppTheme;
        set => MauiTheme.CurrentAppTheme = value;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        MauiTheme.Default.CurrentAppTheme = MauiAppTheme.Dark;
    }
}
