using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Unicorn.LiveLogViewer;

/// <summary>
/// Defines the extension methods that allow to add the Live Log Viewer to a <see cref="IApplicationBuilder"/>
/// </summary>
public static class LiveLogViewerApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the Live Log Viewer required dependencies and allows to
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsBuilder">The <see cref="Action{LiveLogViewerOptions}"/> that allows to configure the options.</param>
    /// <returns></returns>
    public static IServiceCollection AddLiveLogViewer(
        this IServiceCollection services,
        Action<IServiceProvider, LiveLogViewerOptions>? optionsBuilder = null)
    {
        if (optionsBuilder != null)
        {
            services.AddSingleton(sp =>
            {
                var options = new LiveLogViewerOptions();
                optionsBuilder(sp, options);
                return options;
            });
        }
        else
        {
            services.AddSingleton(new LiveLogViewerOptions());
        }

        return services;
    }

    /// <summary>
    /// Inserts the Live Log Viewer middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to configure.</param>
    /// <param name="basePath">The optional base path for all the endpoints. The viewer page will be visible at this address.</param>
    /// <returns>The specified <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseLiveLogViewer(
        this IApplicationBuilder app,
        string basePath = "/logViewer")
    {
        return app;
    }
}