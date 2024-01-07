namespace MauiTheme.Test;
public static class Helper
{
    public static void ChangeThemeResource(ResourceDictionary[] resources)
    {
        Application.Current?.Resources.MergedDictionaries.Clear();
        foreach (ResourceDictionary resource in resources)
        {
            Application.Current?.Resources.MergedDictionaries.Add(resource);
        }
    }
}
