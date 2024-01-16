namespace MauiTheme.Extensions;
internal static class SpanExtension
{
    internal static bool CompareSpanChar(this ReadOnlySpan<char> value, ReadOnlySpan<char> other)
    {
        return value.CompareTo(other, StringComparison.OrdinalIgnoreCase) == 0;
    }
}
