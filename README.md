# Maui Theme

MauiTheme is a theming library that simplifies theming on .NET Maui, providing a seamless experience with persistent theme state between sessions, easy resource swapping, a theme switcher, and support for Blazor Hybrid.

# Packages

Package | Latest stable | Latest Preview | Description
---------|---------------|---------------|------------
`AathifMahir.Maui.MauiTheme` | [![AathifMahir.Maui.MauiTheme](https://img.shields.io/nuget/v/AathifMahir.Maui.MauiTheme)](https://nuget.org/packages/AathifMahir.Maui.MauiTheme/) | [![AathifMahir.Maui.MauiTheme](https://img.shields.io/nuget/vpre/AathifMahir.Maui.MauiTheme)](https://nuget.org/packages/AathifMahir.Maui.MauiTheme/absoluteLatest) | Maui Theme - Theming Libray for Dotnet Maui with Ability Retain Sessions and Easy Resource Swapping and etc.
`AathifMahir.Maui.MauiTheme.BlazorHybrid` | [![AathifMahir.Maui.MauiTheme.BlazorHybrid](https://img.shields.io/nuget/v/AathifMahir.Maui.MauiTheme.BlazorHybrid)](https://nuget.org/packages/AathifMahir.Maui.MauiTheme.BlazorHybrid/) | [![AathifMahir.Maui.MauiTheme.BlazorHybrid](https://img.shields.io/nuget/vpre/AathifMahir.Maui.MauiTheme.BlazorHybrid)](https://nuget.org/packages/AathifMahir.Maui.MauiTheme.BlazorHybrid/absoluteLatest) | Maui Theme - Blazor Hybrid is Extension of MauiTheme with Ability to Switch Theme and Resources from Blazor Hybrid Project without Any Maui Artifacts.

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

        MainPage = new AppShell();

    }
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

# Blazor Hybrid Usage

In Order to Initialize the MauiTheme Blazor Hybrid, you need to call `UseMauiThemeHybrid()` in `program.cs` as shown in the example below:

```csharp
using MauiTheme.BlazorHybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // Initializing MauiTheme on Blazor Hybrid Project by Sharing the Instance of MauiTheme
        builder.Services.UseMauiThemeHybrid(Theme.Default);

        return builder.Build();
    }
}
```

If you are using WebAssembly or any other Blazor hosting model, and you are sharing a Razor Class Library within those, as well as with Blazor Hybrid, you need call `UseMauiThemeBlazor` in those Classic Blazor Project to Avoid Runtime Crashing like below example:

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.UseMauiThemeBlazor();
```



# Theme

When it comes to switching themes, you can change the `CurrentTheme` property to switch the theme like in the example below:

```csharp

// Maui
// --------------------------------------------------

// Dark
Theme.Default.CurrentTheme = ThemeMode.Dark;

// Light
Theme.Default.CurrentTheme = ThemeMode.Light;

// System
Theme.Default.CurrentTheme = ThemeMode.UnSpecified;

// Blazor Hybrid
// ---------------------------------------------------

@inject IThemeHybrid ThemeHybrid

// Dark
ThemeHybrid.CurrentTheme = ThemeMode.Dark;

// Light
ThemeHybrid.CurrentTheme = ThemeMode.Light;

// System
ThemeHybrid.CurrentTheme = ThemeMode.UnSpecified;

```

# Resources

When it comes to switching resources, you can use the `CurrentResource` property to swap the resources, as shown in the example below. Make sure to note that resources are applied using the key that you have passed into the `InitializeTheme` `Resources` property.
```csharp
// Maui
---------------------------------------------------

// Blue.xaml
Theme.Default.CurrentResource = "Blue";

// Purple.xaml
Theme.Default.CurrentResource = "Purple";

// Yellow.xaml
Theme.Default.CurrentResource = "Yellow";


// Blazor Hybrid
---------------------------------------------------

@inject IThemeHybrid ThemeHybrid

ThemeHybrid.CurrentResource = "Blue";

```

# Listening to Theme or Resource Changes in Maui

Mainly this is useful when theme or resource changes is invoked from external source for instance from a razor class library

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

# Listening to Theme or Resource Changes in Blazor Hybrid

This is mainly useful when listening to Theme or Resource Changes from External Sources for Instance from Maui, as you can see in the below example, we are invoking `StateChanged()` method in MauiThemeContext, Basically What that Says is Refresh the Razor Component Whenever Theme Changes

```csharp

@inject IThemeHybrid ThemeHybrid
@implements IDisposable

<title>Theme</title>

<h3>Theme</h3>
<InputRadioGroup Name="Theme" TValue="ThemeMode" @bind-Value="ThemeHybrid.CurrentAppTheme">
    @foreach (var item in (ThemeMode[])Enum.GetValues(typeof(ThemeMode)))
    {
        <InputRadio Name="Theme" TValue="ThemeMode" value="@item"/>
         @item
        <br/>
    }
</InputRadioGroup>

<br />

<h3>Color</h3>
<InputRadioGroup TValue="string" @bind-Value="ThemeHybrid.CurrentResource">
    <InputRadio TValue="string" value="Blue"/>Blue<br/>
    <InputRadio TValue="string" value="Purple"/>Purple<br/>
    <InputRadio TValue="string" value="Yellow"/>Yellow<br/>
    <InputRadio TValue="string" value="Green"/>Green<br />
</InputRadioGroup>


@code{
    ThemeContext? themeContext;

    protected override void OnInitialized()
    {
        themeContext = ThemeContext.Create(ThemeHybrid, () => StateHasChanged());

        base.OnInitialized();
    }

    public void Dispose()
    {
        themeContext?.Dispose();
    }
}

```

# Properties and Methods - MauiTheme

| Parameters | Type | Description |
|               :---               |    :---:   |            :---:       
| **InitializeTheme()** | `method` | This is used for Initializing MauiTheme ||
| **CurrentTheme** | `ThemeMode` | Gets or sets the current theme |
| **CurrentResource** | `string` | Gets or sets the current resource |
| **ThemeChanged** | `event` | Theme Changed event is fired whenever theme changes happens |
| **ResourceChanged** | `event` | Resource Changed event is fired whenever resource changes happens |
| **ThemeChangedCommand** | `ICommand` | Theme Changed Command is fired whenever theme changes happens |
| **ResourceChangedCommand** | `ICommand` | Resource Changed Command is fired whenever resource changes happens |

# Properties and Methods - Hybrid

| Parameters | Type | Description |
|               :---               |    :---:   |            :---:       
| **CurrentTheme** | `ThemeMode` | Gets or sets the current theme |
| **CurrentResource** | `string` | Gets or sets the current resource |
| **ThemeContext.Create()** | `method` | Theme Context would trigger CallBack Whenever Theme Changes Happens Outside Blazor Context |


# License

Maui Theme is Licensed Under [MIT License](https://github.com/AathifMahir/MauiTheme/blob/master/LICENSE.txt).

# Contribute and Credit

Credits for [@taublast](https://github.com/taublast) for Helping with Resource Creation.

If you wish to contribute to this project, please don't hesitate to create an issue or request. Your input and feedback are highly appreciated. Additionally, if you're interested in supporting the project by providing resources or [**becoming a sponsor**](https://github.com/sponsors/AathifMahir), your contributions would be welcomed and instrumental in its continued development and success. Thank you for your interest in contributing to this endeavor.