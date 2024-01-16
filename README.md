# Maui Theme

MauiTheme is Theming Libray that Makes the Theming on Dotnet Maui a Breeze with Persistent Theme State Between Sessions and Seamless Resource Swapping, Theme Switcher and Blazor Hybrid Support.

# Packages

Package | Latest stable | Latest Preview | Description
---------|---------------|---------------|------------
`AathifMahir.Maui.MauiTheme` | [![AathifMahir.Maui.MauiTheme](https://img.shields.io/nuget/v/AathifMahir.Maui.MauiTheme)](https://nuget.org/packages/AathifMahir.Maui.MauiTheme/) | [![AathifMahir.Maui.MauiTheme](https://img.shields.io/nuget/vpre/AathifMahir.Maui.MauiTheme)](https://nuget.org/packages/AathifMahir.Maui.MauiTheme/absoluteLatest) | Maui Theme - Theming Libray for Dotnet Maui with Ability Retain Sessions and Easy Resource Swapping and etc.
`AathifMahir.Maui.MauiTheme.BlazorHybrid` | [![AathifMahir.Maui.MauiTheme.BlazorHybrid](https://img.shields.io/nuget/v/AathifMahir.Maui.MauiTheme.BlazorHybrid)](https://nuget.org/packages/AathifMahir.Maui.MauiTheme.BlazorHybrid/) | [![AathifMahir.Maui.MauiTheme.BlazorHybrid](https://img.shields.io/nuget/vpre/AathifMahir.Maui.MauiTheme.BlazorHybrid)](https://nuget.org/packages/AathifMahir.Maui.MauiTheme.BlazorHybrid/absoluteLatest) | Maui Theme - Blazor Hybrid is Extension of MauiTheme with Ability to Switch Theme and Resources from Blazor Hybrid Project without Any Maui Artifacts.

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
            x.DefaultTheme = MauiAppTheme.Dark;
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

# Blazor Hybrid Usage

In Order to Initialize the MauiTheme Blazor Hybrid, You need to call `UseMauiThemeHybrid()` in `program.cs` like below example

```csharp
using MauiTheme.BlazorHybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // Initializing MauiTheme on Blazor Hybrid Project by Sharing the Instance of MauiTheme
        builder.Services.UseMauiThemeHybrid(MauiTheme.Default);

        return builder.Build();
    }
}
```

If your using WebAssembly or Any Other Blazor Hosting Model, Where your sharing a razor class library within those and Blazor Hybrid

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.UseMauiThemeBlazor();
```



# Theme

When it comes to Switching Theme, You can change the `CurrentTheme` Property to Switch the Theme Like Below Example

```csharp

// Maui
// --------------------------------------------------

// Dark
Theme.Default.CurrentTheme = MauiAppTheme.Dark;

// Light
Theme.Default.CurrentTheme = MauiAppTheme.Light;

// System
Theme.Default.CurrentTheme = MauiAppTheme.UnSpecified;

// Blazor Hybrid
// ---------------------------------------------------

@inject IMauiThemeHybrid MauiThemeHybrid

// Dark
MauiThemeHybrid.CurrentTheme = MauiAppTheme.Dark;

// Light
MauiThemeHybrid.CurrentTheme = MauiAppTheme.Light;

// System
MauiThemeHybrid.CurrentTheme = MauiAppTheme.UnSpecified;

```

# Resources

When it comes to Switching Resource, You can use `CurrentResource` Property to Swap the Resources Like Below Example, Make sure to Note that Resources is Applied Using The Key that you have passed into `InitializeTheme` `Resources` Property

```csharp
// Maui
// ---------------------------------------------------

// Blue.xaml
Theme.Default.CurrentResource = "Blue";

// Purple.xaml
Theme.Default.CurrentResource = "Purple";

// Yellow.xaml
Theme.Default.CurrentResource = "Yellow";

// Blazor Hybrid
// ---------------------------------------------------

@inject IMauiThemeHybrid MauiThemeHybrid

MauiThemeHybrid.CurrentResource = "Blue";

```

# Listening to Theme or Resource Changes in Maui

Mainly this is useful when theme or resource changes is invoked from external source for instance from a razor class library

```csharp

// Theme Changed Event
MauiTheme.Default.ThemeChanged += (s, t) => 
{
    Debug.Writeline($"New Theme : {t.ToString()}")
}

// Theme Changed Event
MauiTheme.Default.ResourceChanged += (s, r) => 
{
    Debug.Writeline($"New Resource : {r}")
}

```

# Listening to Theme or Resource Changes in Blazor Hybrid

This is mainly useful when listening to Theme or Resource Changes from External Sources for Instance from Maui, as you can see in the below example, we are invoking `StateChanged()` method in MauiThemeContext, Basically What that Says is Refresh the Razor Component Whenever Theme Changes

```razor

@inject IMauiThemeHybrid MauiThemeHybrid
@implements IDisposable

<title>Theme</title>

<h3>Theme</h3>
<InputRadioGroup Name="Theme" TValue="MauiAppTheme" @bind-Value="MauiThemeHybrid.CurrentAppTheme">
    @foreach (var item in (MauiAppTheme[])Enum.GetValues(typeof(MauiAppTheme)))
    {
        <InputRadio Name="Theme" TValue="MauiAppTheme" value="@item"/>
         @item
        <br/>
    }
</InputRadioGroup>

<br />

<h3>Color</h3>
<InputRadioGroup TValue="string" @bind-Value="MauiThemeHybrid.CurrentResource">
    <InputRadio TValue="string" value="Blue"/>Blue<br/>
    <InputRadio TValue="string" value="Purple"/>Purple<br/>
    <InputRadio TValue="string" value="Yellow"/>Yellow<br/>
    <InputRadio TValue="string" value="Green"/>Green<br />
</InputRadioGroup>


@code{
    MauiThemeContext? themeContext;

    protected override void OnInitialized()
    {
        themeContext = MauiThemeContext.Create(MauiThemeHybrid, () => StateHasChanged());

        base.OnInitialized();
    }

    public void Dispose()
    {
        themeContext?.Dispose();
    }
}

```


# License

Maui Theme is Licensed Under [MIT License](https://github.com/AathifMahir/MauiTheme/blob/master/LICENSE.txt).

# Contribute and Credit

Credits for [@taublast](https://github.com/taublast) for Helping with Resource Creation.

If you wish to contribute to this project, please don't hesitate to create an issue or request. Your input and feedback are highly appreciated. Additionally, if you're interested in supporting the project by providing resources or [**becoming a sponsor**](https://github.com/sponsors/AathifMahir), your contributions would be welcomed and instrumental in its continued development and success. Thank you for your interest in contributing to this endeavor.