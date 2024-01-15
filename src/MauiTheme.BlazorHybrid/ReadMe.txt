# Maui Theme Blazor Hybrid

MauiTheme Blazor Hybrid is Extension of Vanilla MauiTheme Library Where it Provides Access the MauiTheme Without Any Maui Artifacts Inside a Razor Class Library

**Disclaimer:** You need to have [MauiTheme Library](https://www.nuget.org/packages/AathifMahir.Maui.MauiTheme) Installed on Your Maui Project and `InitializeTheme()` Method is Called in `App.xaml.cs`

# Get Started

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

@inject IMauiThemeHybrid MauiThemeHybrid

// Pass in your resource key that assigned in Maui Project
MauiThemeHybrid.CurrentResource = "Blue";

```

# Listening to Theme or Resource Changes

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

MauiTheme is Licensed Under [MIT License](https://github.com/AathifMahir/MauiTheme/blob/master/LICENSE.txt).

# Contribute and Credit

Credits for [@taublast](https://github.com/taublast) for Helping with Resource Creation.

If you wish to contribute to this project, please don't hesitate to create an issue or request. Your input and feedback are highly appreciated. Additionally, if you're interested in supporting the project by providing resources or [**becoming a sponsor**](https://github.com/sponsors/AathifMahir), your contributions would be welcomed and instrumental in its continued development and success. Thank you for your interest in contributing to this endeavor.

#### For More Information and Complete Docs, [Visit the repo](https://github.com/AathifMahir/MauiTheme)