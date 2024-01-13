using MauiTheme.Core;
using MauiTheme.Exceptions;
using MauiTheme.Extensions;
using MauiTheme.Helpers;
using System.Reflection;
using System.Text;
using System.Text.Json;


namespace MauiTheme;
internal sealed class MauiThemeDefault : IMauiTheme
{

    private MauiAppTheme _currentAppTheme;
    public MauiAppTheme CurrentAppTheme
    {
        get => _currentAppTheme;
        set
        {
            if (value != _currentAppTheme)
            {
                _currentAppTheme = value;
                SetTheme(value);
            }
        }
    }

    string _currentResource = string.Empty;
    public string CurrentResource
    {
        get => _currentResource;
        set
        {
            if (value != _currentResource)
            {
                _currentResource = value;
                SetResource(value);
            }
        }
    }

    // Internal Fields and Properities
    const string _storageKey = "Theme";
    ThemeStorage _themeStorage = new();
    Dictionary<string, string> _resources = new();
    MauiAppTheme? _defaultTheme = null;
    string[] _defaultStyleResources = Array.Empty<string>();
    Assembly _appAssembly = typeof(MauiThemeDefault).Assembly;
    bool _isInitialized = false;

    public void InitializeTheme<TApp>(Action<ThemeConfiguration> themeConfiguration)
    {
        if (_isInitialized) return;

        _isInitialized = true;
        ThemeConfiguration theme = new();
        themeConfiguration(theme);

        _resources = theme.Resources;
        _defaultTheme = theme.DefaultTheme;
        _defaultStyleResources = theme.DefaultStyleResources;
        _appAssembly = typeof(TApp).Assembly;

        var json = Preferences.Default.Get(_storageKey, string.Empty).AsSpan();

        if (json.IsEmpty)
        {
            _currentResource = GetInitialResource();
            return;
        }

        _themeStorage = JsonSerializer.Deserialize<ThemeStorage>(json) ?? new() {
            AppTheme = _defaultTheme.Value,
            Resource = string.Empty };

        if (!_themeStorage.AppTheme.HasValue)
            ApplyTheme(_defaultTheme.Value);
        else
            ApplyTheme(_themeStorage.AppTheme.Value);

        if (!string.IsNullOrEmpty(_themeStorage.Resource))
            ApplyResource(_themeStorage.Resource, isInit: true);

    }

    void ApplyResource(string resource, bool isInit = false)
    {
        var resourceSpan = resource.AsSpan();
        if (resourceSpan.IsEmpty) return;

        if (Application.Current is null) return;

        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

        if (mergedDictionaries is null || mergedDictionaries.Count is 0) return;

        if (isInit && mergedDictionaries
            .First().Source.OriginalString.AsSpan().CompareSpanChar(GenerateFullUriString(resource)))
        {
            _currentResource = _resources.FirstOrDefault(x => x.Value.AsSpan().CompareSpanChar(resource.AsSpan())).Key;
            return;
        }

        ResourceDictionary? rdPrimary;

        if (isInit)
        {
            rdPrimary = CreateResource(resource);
            _currentResource = _resources.FirstOrDefault(x => x.Value == resource).Key;
        }
        else
        {
            rdPrimary = CreateResource(_resources[resource]);
            _currentResource = resource;
        }


        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(rdPrimary);

        foreach (var val in _defaultStyleResources)
        {
            var rd = CreateResource(val);

            Application.Current.Resources.MergedDictionaries.Add(rd);
        }
    }

    string GetInitialResource()
    {
        if (Application.Current is null) return string.Empty;
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries is null || mergedDictionaries.Count is 0) return string.Empty;
        return _resources.FirstOrDefault(x => GenerateFullUriString(x.Value).CompareSpanChar(mergedDictionaries.First().Source.OriginalString.AsSpan())).Key;
    }

    void SetResource(string resourceKey)
    {
        if (!_isInitialized) throw new MauiThemeException("Make sure to Initialize by using Initialize Theme before Setting the Resource");

        ApplyResource(resourceKey);

        _themeStorage.Resource = _resources[resourceKey];

        Preferences.Default.Set(_storageKey, _themeStorage.ToJson());
    }

    void ApplyTheme(MauiAppTheme theme)
    {
        if (Application.Current is null) return;
        var appTheme = EnumHelper.MapMauiTheme(theme);
        if (Application.Current.UserAppTheme == appTheme) return;

        Application.Current.UserAppTheme = appTheme;
        _currentAppTheme = theme;
    }

    void SetTheme(MauiAppTheme appTheme)
    {
        if (!_isInitialized) throw new MauiThemeException("Make sure to Initialize by using Initialize Theme before Setting the Theme");

        ApplyTheme(appTheme);

        _themeStorage.AppTheme = appTheme;

        Preferences.Default.Set(_storageKey, _themeStorage.ToJson());
    }

    ResourceDictionary CreateResource(string source)
    {
        var xamlResources = _appAssembly.GetCustomAttributes<XamlResourceIdAttribute>()
            ?? Enumerable.Empty<XamlResourceIdAttribute>();

        if (!xamlResources.Any())
            throw new MauiThemeException("No XAML resources found. Please ensure proper resource URIs and existing resource dictionaries in your project.");

        var sourceSpan = source.AsSpan();
        if (sourceSpan.IsEmpty) throw new MauiThemeException($"Resource with path not found. Make sure the provide correct resource path.");

        var matchingResource = xamlResources.FirstOrDefault(x => x.Path.AsSpan().CompareSpanChar(source.AsSpan()))?.Type
            ?? throw new MauiThemeException($"Resource with path '{sourceSpan}' not found. Make sure the resource dictionary exists.");

        return Activator.CreateInstance(matchingResource) is not ResourceDictionary rd
            ? throw new MauiThemeException($"Resource with path '{sourceSpan}' not found. Make sure the resource dictionary exists.")
            : rd;
    }


    ReadOnlySpan<char> GenerateFullUriString(ReadOnlySpan<char> resource)
    {
        ReadOnlySpan<char> assemblyName = _appAssembly.GetName().Name.AsSpan();
        int length = resource.Length + assemblyName.Length + 10;

        Span<char> assemblyPrefix = stackalloc char[10];

        assemblyPrefix[0] = ';';
        assemblyPrefix[1] = 'a';
        assemblyPrefix[2] = 's';
        assemblyPrefix[3] = 's';
        assemblyPrefix[4] = 'e';
        assemblyPrefix[5] = 'm';
        assemblyPrefix[6] = 'b';
        assemblyPrefix[7] = 'l';
        assemblyPrefix[8] = 'y';
        assemblyPrefix[9] = '=';

        StringBuilder stringBuilder = new(length);
        stringBuilder.Append(resource);
        stringBuilder.Append(assemblyPrefix);
        stringBuilder.Append(assemblyName);
        return stringBuilder.ToString().AsSpan();
    }
}
