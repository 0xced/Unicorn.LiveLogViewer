using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Unicorn.LiveLogViewer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLiveLogViewer((sp, options) => options.BasePath = "");

var app = builder.Build();

app.MapLiveLogViewer();

app.Run();