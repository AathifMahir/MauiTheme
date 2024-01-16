using MauiTheme.Core;
using System.Windows.Input;

namespace MauiTheme;
public interface IMauiTheme : IMauiThemeHybrid
{
    /// <summary>
    /// Initializes the Maui Theme
    /// </summary>
    /// <typeparam name="TApp">Maui Application Type</typeparam>
    /// <param name="themeConfiguration">Default Set of Theme and Resources</param>
    void InitializeTheme<TApp>(Action<ThemeConfiguration> themeConfiguration);

    /// <summary>
    /// Theme Changed Command for Detecting Theme Changes at Realtime and also ThemeChanged Command Does Support Theme Change Notification from External Sources
    /// </summary>
    /// <remarks>
    /// Returns MauiAppTheme as Parameter
    /// </remarks>
    ICommand? ThemeChangedCommand { get; set; }

    /// <summary>
    /// Resource Changed Command for Detecting Resource Changes at Realtime and also ResourceChanged Command Does Support Resource Change Notification from External Sources
    /// </summary>
    /// <remarks>
    /// Returns string as Parameter
    /// </remarks>
    ICommand? ResourceChangedCommand { get; set; }
}
