using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Unicorn.LiveLogViewer.Middleware;

/// <summary>
/// The middleware that serves the Live Log Viewer requests.
/// </summary>
public class LiveLogViewerMiddleware : IMiddleware
{
    /// <summary>
    /// Request handling method.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
    /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.GetTypedHeaders().ContentType = new MediaTypeHeaderValue(System.Net.Mime.MediaTypeNames.Text.Plain);
        await context.Response.WriteAsync($"Hello: {context.Request.Path}");
    }
}