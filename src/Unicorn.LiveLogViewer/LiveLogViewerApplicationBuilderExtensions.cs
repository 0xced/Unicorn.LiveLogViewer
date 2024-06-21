using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Unicorn.LiveLogViewer.Middleware;

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

        services.AddScoped<LiveLogViewerMiddleware>();

        return services;
    }
/*
    /// <summary>
    /// Inserts the Live Log Viewer middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to configure.</param>
    /// <param name="basePath"></param>
    /// <returns>The specified <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseLiveLogViewer(
        this IApplicationBuilder app, string basePath = "/LogViewer")
    {
        return app.Map(basePath, builder => builder.UseMiddleware<LiveLogViewerMiddleware>());
    }
*/
    /// <summary>
    /// Inserts the Live Log Viewer middleware to the application pipeline.
    /// </summary>
    /// <param name="endpoints"></param>
    /// <param name="basePath"></param>
    /// <returns></returns>
    public static IEndpointConventionBuilder MapLiveLogViewer(this IEndpointRouteBuilder endpoints, string basePath = "/LogViewer/{*path}")
    {
        var pipeline = endpoints.CreateApplicationBuilder()
            .UseMiddleware<LiveLogViewerMiddleware>()
            .Build();

        return endpoints.MapGet(basePath, pipeline);
    }
}