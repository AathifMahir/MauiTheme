using MauiTheme.Core;

namespace MauiTheme;
public interface IMauiTheme : IMauiThemeHybrid
{
    /// <summary>
    /// Initializes the Maui Theme
    /// </summary>
    /// <typeparam name="TApp">Maui Application Type</typeparam>
    /// <param name="themeConfiguration">Default Set of Theme and Resources</param>
    void InitializeTheme<TApp>(Action<ThemeConfiguration> themeConfiguration);
}
