# Maui Theme

|**Latest Stable** | **Latest Preview**|
|  :---:     |    :---:   |
|[![AathifMahir.Maui.MauiShakeDetector](https://img.shields.io/nuget/v/AathifMahir.Maui.MauiTheme)](https://www.nuget.org/packages/AathifMahir.Maui.MauiTheme/) | [![AathifMahir.Maui.MauiShakeDetector](https://img.shields.io/nuget/vpre/AathifMahir.Maui.MauiTheme)](https://nuget.org/packages/AathifMahir.Maui.MauiTheme/absoluteLatest) |

MauiTheme is Theming Libray that Makes the Theming on Dotnet Maui a Breeze with Persistent Theme State Between Sessions and Seamless Resource Swapping, Theme Switcher and etc..

# Get Started

You need to Call `InitializeTheme()` in the `App.xaml.cs` Like Below Example

```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        Theme.Default.InitializeTheme<App>(x =>
        {
            // Default Theme
            x.DefaultTheme = AppTheme.Dark;
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
```

# App.xaml Setup

The App.xaml should include the Default Color and Style Resource Like Below Example

```xml
<Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
            <!-- Always Color Resources At First Before Style Resources -->
                <ResourceDictionary Source="Resources/Styles/Yellow.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
</Application.Resources>
```

# Initialize Properties

| Parameters | Type | Description |
|               :---               |    :---:   |            :---:                                                                               |
| **DefaultTheme** | `enum` | The Default Theme, if the Theme is Previously Not Assigned by User |
| **DefaultStyleResources** | `Dictionary<string, string>` | The Default Style Resources that Needs to Applied with Every Resource Change **Eg: Styles.xaml** |
| **Resources** | `Dictionary<string, string>` | All Resources in the Project Excluding Style Resources |

# Theme

When it comes to Switching Theme, You can change the `CurrentTheme` Property to Switch the Theme Like Below Example

```csharp

// Dark
Theme.Default.CurrentTheme = AppTheme.Dark;

// Light
Theme.Default.CurrentTheme = AppTheme.Light;

// System
Theme.Default.CurrentTheme = AppTheme.UnSpecified;

```

# Resources

When it comes to Switching Resource, You can use `CurrentResource` Property to Swap the Resources Like Below Example, Make sure to Note that Resources is Applied Using The Key that you have passed into `InitializeTheme` `Resources` Property

```csharp

// Blue.xaml
Theme.Default.CurrentResource = "Blue";

// Purple.xaml
Theme.Default.CurrentResource = "Purple";

// Yellow.xaml
Theme.Default.CurrentResource = "Yellow";

```


# License

Maui Shake Detector is Licensed Under [MIT License](https://github.com/AathifMahir/MauiTheme/blob/master/LICENSE.txt).

# Contribute and Credit

Credits for @taublast for Helping with Resource Creation.

If you wish to contribute to this project, please don't hesitate to create an issue or request. Your input and feedback are highly appreciated. Additionally, if you're interested in supporting the project by providing resources or [**becoming a sponsor**](https://github.com/sponsors/AathifMahir), your contributions would be welcomed and instrumental in its continued development and success. Thank you for your interest in contributing to this endeavor.