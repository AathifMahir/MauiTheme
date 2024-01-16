using MauiTheme.Core;

namespace MauiTheme.Sample.BlazorHybrid;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MauiTheme.Default.InitializeTheme<App>(x =>
        {
            // Default Theme
            x.DefaultTheme = MauiAppTheme.Light;
            // Default Styles Resources
            x.DefaultStyleResources = ["Resources/Styles/Styles.xaml"];
            // All Resources Excluding Styles
            x.Resources = new()
                {
                    {"Blue", "Resources/Styles/Blue.xaml"},
                    {"Purple", "Resources/Styles/Colors.xaml"},
                    {"Yellow", "Resources/Styles/Yellow.xaml" },
                    {"Green", "Resources/Styles/Green.xaml" }
                };
        });

        MainPage = new MainPage();

        
    }
}
