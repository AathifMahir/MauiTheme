
namespace MauiTheme.Core;
public interface IMauiTheme : IMauiThemeHybrid
{
    void InitializeTheme<TApp>(Action<ThemeConfiguration> themeConfiguration);
}
