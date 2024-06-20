namespace Unicorn.LiveLogViewer;

/// <summary>
/// Provides options for the Live Log Viewer.
/// </summary>
public class LiveLogViewerOptions
{
    /// <summary>
    /// The base path at which the Live Log Viewer middleware will listen.
    /// This will also be the address of the log viewer web interface.
    /// </summary>
    /// <remarks>Default value: <c>"/LogViewer"</c>.</remarks>
    public string BasePath { get; set; } = "/LogViewer";
}