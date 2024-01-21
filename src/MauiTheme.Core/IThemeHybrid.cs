

using MauiTheme.Core.Events;

namespace MauiTheme.Core;
public interface IThemeHybrid
{
    /// <summary>
    /// Get or Set Current App Theme
    /// </summary>
    public ThemeMode CurrentAppTheme { get; set; }

    /// <summary>
    /// Get or Set Current App Resource using the Key that Used on Theme Initialization
    /// </summary>
    public string CurrentResource { get; set; }

    /// <summary>
    /// Theme Changed Event for Detecting Theme Changes at Realtime and also ThemeChanged Event Does Support Theme Change Notification from External Sources
    /// </summary>

    event EventHandler<ThemeModeChangedEventArgs>? ThemeChanged;

    /// <summary>
    /// Resource Changed Event for Detecting Resource Changes at Realtime and also ResourceChanged Event Does Support Resource Change Notification from External Sources
    /// </summary>

    event EventHandler<ResourceChangedEventArgs>? ResourceChanged;



}
