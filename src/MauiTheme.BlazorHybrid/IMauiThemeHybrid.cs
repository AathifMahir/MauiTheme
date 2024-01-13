using MauiTheme.Core;

namespace MauiTheme.BlazorHybrid;
public interface IMauiThemeHybrid
{
    public MauiAppTheme CurrentAppTheme { get; set; }
    public string CurrentResource { get; set; }
}
