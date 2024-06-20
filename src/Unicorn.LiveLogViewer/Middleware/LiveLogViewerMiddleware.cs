using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Unicorn.LiveLogViewer.Middleware;

/// <summary>
/// The middleware that serves the Live Log Viewer requests.
/// </summary>
#if !NETSTANDARD
public class LiveLogViewerMiddleware : IMiddleware
#else
public class LiveLogViewerMiddleware
#endif
{
    /// <summary>
    /// Request handling method.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
    /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }
}