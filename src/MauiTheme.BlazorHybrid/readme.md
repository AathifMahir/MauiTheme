# Maui Theme Blazor Hybrid

**MauiTheme Blazor Hybrid** is an extension of the Vanilla MauiTheme Library, providing access to the MauiTheme without any Maui artifacts inside a Razor Class Library.

**Disclaimer:** You need to have the [MauiTheme Library](https://www.nuget.org/packages/AathifMahir.Maui.MauiTheme) installed in your Maui project, and the `InitializeTheme()` method must be called in `App.xaml.cs`.

# Get Started

To initialize the MauiTheme Blazor Hybrid, you need to call `UseMauiThemeHybrid()` in `program.cs` as shown in the example below:

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

If your using WebAssembly or Any Other Blazor Hosting Model, Where your sharing a razor class library within those and Blazor Hybrid

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.UseMauiThemeBlazor();
```

**Disclaimer:** Calling `UseMauiThemeBlazor()` won't make the MauiTheme available for Classic Blazor Hosting Models. Instead, this would prevent runtime exceptions if any property is changed outside of Blazor Hybrid context.


# Theme

When it comes to switching themes, you can change the `CurrentTheme` property to switch the theme like in the example below:

```csharp
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

@inject IThemeHybrid ThemeHybrid

// Pass in your resource key that assigned in Maui Project
ThemeHybrid.CurrentResource = "Blue";

```

# Listening to Theme or Resource Changes

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

# Properties and Methods

| Parameters | Type | Description |
|               :---               |    :---:   |            :---:       
| **CurrentTheme** | `ThemeMode` | Gets or sets the current theme |
| **CurrentResource** | `string` | Gets or sets the current resource |
| **ThemeContext.Create()** | `method` | Theme Context would trigger CallBack Whenever Theme Changes Happens Outside Blazor Context |


# License

MauiTheme is Licensed Under [MIT License](https://github.com/AathifMahir/MauiTheme/blob/master/LICENSE.txt).

# Contribute and Credit

Credits for [@taublast](https://github.com/taublast) for Helping with Resource Creation.

If you wish to contribute to this project, please don't hesitate to create an issue or request. Your input and feedback are highly appreciated. Additionally, if you're interested in supporting the project by providing resources or [**becoming a sponsor**](https://github.com/sponsors/AathifMahir), your contributions would be welcomed and instrumental in its continued development and success. Thank you for your interest in contributing to this endeavor.

#### For More Information and Complete Docs, [Visit the repo](https://github.com/AathifMahir/MauiTheme)