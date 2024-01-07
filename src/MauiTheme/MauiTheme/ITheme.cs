using System.Reflection;

namespace MauiTheme;
public interface ITheme
{
    public AppTheme CurrentAppTheme { get; set; }
    public string CurrentResource { get; set; }
    void InitializeTheme<TApp>(Action<ThemeConfiguration> themeConfiguration) where TApp : Application;
}
