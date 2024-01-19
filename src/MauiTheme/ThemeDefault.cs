using MauiTheme.Core;
using MauiTheme.Core.Exceptions;
using MauiTheme.Core.Events;
using MauiTheme.Extensions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Windows.Input;


namespace MauiTheme;
internal sealed class ThemeDefault : ITheme
{

    private ThemeMode _currentAppTheme;
    public ThemeMode CurrentAppTheme
    {
        get => _currentAppTheme;
        set
        {
            if (value != _currentAppTheme)
            {
                _currentAppTheme = value;
                SetTheme(value);

                ThemeChanged?.Invoke(this, new MauiAppThemeChangedEventArgs(value));

                if (ThemeChangedCommand is not null && ThemeChangedCommand.CanExecute(value))
                    ThemeChangedCommand.Execute(value);
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

                ResourceChanged?.Invoke(this, new ResourceChangedEventArgs(value));

                if (ResourceChangedCommand is not null && ResourceChangedCommand.CanExecute(value))
                    ResourceChangedCommand.Execute(value);
            }
        }
    }

    public ICommand? ThemeChangedCommand { get; set; }
    public ICommand? ResourceChangedCommand { get; set; }

    // Internal Fields and Properities
    const string _storageKey = "Theme";
    ThemeStorage _themeStorage = new();
    Dictionary<string, string> _resources = new();
    ThemeMode _defaultTheme;
    string[] _defaultStyleResources = Array.Empty<string>();
    Assembly _appAssembly = typeof(ThemeDefault).Assembly;
    bool _isInitialized = false;

    public event EventHandler<MauiAppThemeChangedEventArgs>? ThemeChanged;
    public event EventHandler<ResourceChangedEventArgs>? ResourceChanged;

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
            if (_defaultTheme is ThemeMode.Unspecified) return;
            ApplyTheme(_defaultTheme);
            return;
        }

        _themeStorage = JsonSerializer.Deserialize<ThemeStorage>(json) ?? new() {
            AppTheme = _defaultTheme,
            Resource = string.Empty };

        ApplyTheme(_themeStorage.AppTheme);

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

    void ApplyTheme(ThemeMode theme)
    {
        if (Application.Current is null) return;
        var appTheme = theme.MapToAppTheme();
        if (Application.Current.UserAppTheme == appTheme) return;

        Application.Current.UserAppTheme = appTheme;
        _currentAppTheme = theme;
    }

    void SetTheme(ThemeMode appTheme)
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

        if (xamlResources == Enumerable.Empty<XamlResourceIdAttribute>())
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
