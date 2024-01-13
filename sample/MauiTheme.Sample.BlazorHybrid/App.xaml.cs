using MauiTheme.Core;

namespace MauiTheme.Sample.BlazorHybrid;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();

        MauiTheme.Default.InitializeTheme<App>(x =>
        {
            x.DefaultTheme = MauiAppTheme.Dark;
        });
    }
}
