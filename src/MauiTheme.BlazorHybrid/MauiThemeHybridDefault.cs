using MauiTheme.Core;

namespace MauiTheme.BlazorHybrid;
internal sealed class MauiThemeHybridDefault : IMauiThemeHybrid
{
    public MauiAppTheme CurrentAppTheme 
    {
        get => _clientTheme.CurrentAppTheme;
        set => _clientTheme.CurrentAppTheme = value;
    }
    public string CurrentResource 
    {
        get => _clientTheme.CurrentResource;
        set => _clientTheme.CurrentResource = value;
    }

    private readonly IMauiTheme _clientTheme;
    public MauiThemeHybridDefault(IMauiTheme clientTheme)
    {
        _clientTheme = clientTheme;
    }
}
