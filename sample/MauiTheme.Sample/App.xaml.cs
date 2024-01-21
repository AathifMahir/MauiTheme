using MauiTheme.Core;

namespace MauiTheme.Sample;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        Theme.Default.InitializeTheme<App>(x =>
        {
            // Default Theme
            x.DefaultTheme = ThemeMode.Dark;
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

    }
}
