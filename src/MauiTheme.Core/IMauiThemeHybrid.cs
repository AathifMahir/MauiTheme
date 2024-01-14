

namespace MauiTheme.Core;
public interface IMauiThemeHybrid
{
    /// <summary>
    /// Get or Set Current App Theme
    /// </summary>
    public MauiAppTheme CurrentAppTheme { get; set; }

    /// <summary>
    /// Get or Set Current App Resource using the Key that Used on Theme Initialization
    /// </summary>
    public string CurrentResource { get; set; }
}
