namespace MauiTheme.Core.Exceptions;
public sealed class MauiThemeException : Exception
{
    public MauiThemeException(string message) : base(message) { }
    public MauiThemeException(string code, string message) : base($"{code}: {message}") { }
}
