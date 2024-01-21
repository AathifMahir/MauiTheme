# Maui Theme

MauiTheme is a theming library that makes theming on .NET MAUI a breeze, providing persistent theme state between sessions and seamless resource swapping, theme switcher, and more.

# Get Started

You need to call `InitializeTheme()` in the `App.xaml.cs` file as shown in the example below. Ensure that `InitializeTheme()` is called before setting up the MainPage property.

```csharp
using MauiTheme.Core;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Theme.Default.InitializeTheme<App>(x =>
        {
            // Default Theme
            x.DefaultTheme = ThemeMode.Dark;
            // Default Styles Resources
            x.DefaultStyleResources = new List<string> { "Resources/Styles/Styles.xaml" };
            // All Resources Excluding Styles
            x.Resources = new Dictionary<string, string>
            {
                {"Blue", "Resources/Styles/Blue.xaml"},
                {"Purple", "Resources/Styles/Colors.xaml"},
                {"Yellow", "Resources/Styles/Yellow.xaml" },
                {"Green", "Resources/Styles/Green.xaml" }
            };
        });

        MainPage = new AppShell();
    }
```

# App.xaml Setup

The App.xaml should include the default color and style resources as shown below:

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

When it comes to switching themes, you can change the `CurrentTheme` property to switch the theme as shown below:

```csharp

// Dark
Theme.Default.CurrentTheme = ThemeMode.Dark;

// Light
Theme.Default.CurrentTheme = ThemeMode.Light;

// System
Theme.Default.CurrentTheme = ThemeMode.UnSpecified;

```

# Resources

When it comes to switching resources, you can use the `CurrentResource` property to swap the resources.

**Dislaimer:** the resources are applied using the key passed into the InitializeTheme `Resources` property:

```csharp

// Blue.xaml
Theme.Default.CurrentResource = "Blue";

// Purple.xaml
Theme.Default.CurrentResource = "Purple";

// Yellow.xaml
Theme.Default.CurrentResource = "Yellow";

```

# Listening to Theme or Resource Changes

This is mainly useful when theme or resource changes are invoked from an external source, such as a razor class library but nothing stopping from using it tradionally:

```csharp

// Theme Changed Event
Theme.Default.ThemeChanged += (s, t) => 
{
    Debug.Writeline($"New Theme : {t.ToString()}")
}

// Theme Changed Event
Theme.Default.ResourceChanged += (s, r) => 
{
    Debug.Writeline($"New Resource : {r}")
}

```

Additionally, we can use `ICommand` as well, those are `ThemeChangedCommand` and `ResourceChangedCommand`.

# Properties and Methods

| Parameters | Type | Description |
|               :---               |    :---:   |            :---:       
| **InitializeTheme()** | `method` | This is used for Initializing MauiTheme ||
| **CurrentTheme** | `ThemeMode` | Gets or sets the current theme |
| **CurrentResource** | `string` | Gets or sets the current resource |
| **ThemeChanged** | `event` | Theme Changed event is fired whenever theme changes happens |
| **ResourceChanged** | `event` | Resource Changed event is fired whenever resource changes happens |
| **ThemeChangedCommand** | `ICommand` | Theme Changed Command is fired whenever theme changes happens |
| **ResourceChangedCommand** | `ICommand` | Resource Changed Command is fired whenever resource changes happens |


# License

Maui Theme is Licensed Under [MIT License](https://github.com/AathifMahir/MauiTheme/blob/master/LICENSE.txt).

# Contribute and Credit

Credits to [@taublast](https://github.com/taublast) for Helping with Resource Creation.

If you wish to contribute to this project, please don't hesitate to create an issue or request. Your input and feedback are highly appreciated. Additionally, if you're interested in supporting the project by providing resources or [**becoming a sponsor**](https://github.com/sponsors/AathifMahir), your contributions would be welcomed and instrumental in its continued development and success. Thank you for your interest in contributing to this endeavor.