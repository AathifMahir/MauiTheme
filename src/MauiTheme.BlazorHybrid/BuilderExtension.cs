using MauiTheme.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MauiTheme.BlazorHybrid;
public static class BuilderExtension
{
    public static IServiceCollection UseMauiThemeHybrid(this IServiceCollection services, IMauiTheme clientTheme, DependencyType dependencyType = DependencyType.Scoped)
    {
        if (dependencyType == DependencyType.Scoped)
            services.AddScoped<IMauiThemeHybrid>((_) => new MauiThemeHybridDefault(clientTheme));
        else
            services.AddSingleton<IMauiThemeHybrid>(new MauiThemeHybridDefault(clientTheme));

        return services;
    }
}
