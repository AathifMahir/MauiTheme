namespace MauiTheme.Core.Events;
public sealed class ResourceChangedEventArgs
{
    public string CurrentResource { get; set; }

    public ResourceChangedEventArgs(string currentResource)
    {
        CurrentResource = currentResource;
    }
}
