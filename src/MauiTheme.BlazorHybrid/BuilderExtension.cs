using MauiTheme.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MauiTheme.BlazorHybrid;
public static class BuilderExtension
{
    /// <summary>
    /// Initializes the Maui Theme for a Blazor Hybrid Project. Ensure not to use this BuilderExtension on Classic Blazor Projects.
    /// </summary>
    /// <param name="clientTheme">An instance of IMauiThemeHybrid that needs to be provided from the Blazor Hybrid Project.</param>
    /// <param name="dependencyType">Indicates whether the dependency should be stored as Scoped or Singleton. Defaults to Scoped.</param>
    public static IServiceCollection UseMauiThemeHybrid(this IServiceCollection services, IMauiThemeHybrid clientTheme, DependencyType dependencyType = DependencyType.Scoped)
    {
        if (dependencyType == DependencyType.Scoped)
            services.AddScoped((_) => clientTheme);
        else
            services.AddSingleton(clientTheme);

        return services;
    }

    /// <summary>
    /// Initializes the Maui Theme for a Classic Blazor Project. Ensure not to use this BuilderExtension on Blazor Hybrid Projects.
    /// </summary>
    /// <param name="suppressException">Indicates whether to suppress exceptions. Defaults to True.</param>
    /// <param name="dependencyType">Indicates whether the dependency should be stored as Scoped or Singleton. Defaults to Scoped.</param>
    public static IServiceCollection UseMauiThemeBlazor(this IServiceCollection services, bool suppressException = true, DependencyType dependencyType = DependencyType.Scoped)
    {
        if (dependencyType == DependencyType.Scoped)
            services.AddScoped<IMauiThemeHybrid>((_) => new MauiThemeHybridDefault(suppressException));
        else
            services.AddSingleton<IMauiThemeHybrid>(new MauiThemeHybridDefault(suppressException));

        return services;
    }

}
