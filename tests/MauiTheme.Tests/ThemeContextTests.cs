using MauiTheme.BlazorHybrid;
using MauiTheme.Core;
using MauiTheme.Core.Events;
using Xunit;

namespace MauiTheme.Tests;

public class ThemeContextTests
{
    [Fact]
    public void Create_ShouldSubscribeToThemeChanged()
    {
        // Arrange
        var mockTheme = new MockThemeHybrid();
        bool callbackInvoked = false;
        Action callback = () => callbackInvoked = true;

        // Act
        using var context = ThemeContext.Create(mockTheme, callback);
        mockTheme.RaiseThemeChanged(ThemeMode.Dark);

        // Assert
        Assert.True(callbackInvoked);
    }

    [Fact]
    public void Create_ShouldSubscribeToResourceChanged()
    {
        // Arrange
        var mockTheme = new MockThemeHybrid();
        bool callbackInvoked = false;
        Action callback = () => callbackInvoked = true;

        // Act
        using var context = ThemeContext.Create(mockTheme, callback);
        mockTheme.RaiseResourceChanged("Blue");

        // Assert
        Assert.True(callbackInvoked);
    }

    [Fact]
    public void Dispose_ShouldUnsubscribeFromEvents()
    {
        // Arrange
        var mockTheme = new MockThemeHybrid();
        bool callbackInvoked = false;
        Action callback = () => callbackInvoked = true;

        // Act
        using (var context = ThemeContext.Create(mockTheme, callback))
        {
            mockTheme.RaiseThemeChanged(ThemeMode.Light);
        }
        callbackInvoked = false;
        mockTheme.RaiseThemeChanged(ThemeMode.Dark);

        // Assert
        Assert.False(callbackInvoked);
    }

    private sealed class MockThemeHybrid : IThemeHybrid
    {
        public ThemeMode CurrentAppTheme { get; set; } = ThemeMode.Unspecified;
        public string CurrentResource { get; set; } = string.Empty;

        public event EventHandler<ThemeModeChangedEventArgs>? ThemeChanged;
        public event EventHandler<ResourceChangedEventArgs>? ResourceChanged;

        public void RaiseThemeChanged(ThemeMode theme)
        {
            ThemeChanged?.Invoke(this, new ThemeModeChangedEventArgs(theme));
        }

        public void RaiseResourceChanged(string resource)
        {
            ResourceChanged?.Invoke(this, new ResourceChangedEventArgs(resource));
        }
    }
}
