using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Unicorn.LiveLogViewer.Tests;

public class LiveLogViewerApplicationBuilderExtensionsTest
{
    private readonly ServiceCollection _serviceCollection = [];

    [Fact]
    public void AddLiveLogViewer_OptionsBuilderIsNull_ServiceProviderReturnsDefaultOptions()
    {
        // Arrange

        // Act
        _serviceCollection.AddLiveLogViewer();

        // Assert
        var service = _serviceCollection.BuildServiceProvider().GetService<LiveLogViewerOptions>();
        service.Should().BeEquivalentTo(new LiveLogViewerOptions());
    }

    [Fact]
    public void AddLiveLogViewer_OptionsBuilderIsDefined_SetupActionInvokedWhenServiceCreated()
    {
        // Arrange
        IServiceProvider? receivedServiceProvider = null;
        LiveLogViewerOptions? receivedOptions = null;

        var optionsBuilder = Substitute.For<Action<IServiceProvider, LiveLogViewerOptions>>();
        optionsBuilder.Invoke(
            Arg.Do<IServiceProvider>(x => receivedServiceProvider = x),
            Arg.Do<LiveLogViewerOptions>(x => receivedOptions = x));

        _serviceCollection.AddSingleton("my-service");

        // Act
        _serviceCollection.AddLiveLogViewer(optionsBuilder);

        // Assert
        var provider = _serviceCollection.BuildServiceProvider();
        _ = provider.GetService<LiveLogViewerOptions>();

        optionsBuilder.ReceivedWithAnyArgs(1).Invoke(default!, default!);
        receivedServiceProvider.Should().BeAssignableTo<IServiceProvider>()
            .Which.GetService<string>().Should().BeSameAs("my-service");
        receivedOptions.Should().BeEquivalentTo(new LiveLogViewerOptions());
    }

    [Fact]
    public void AddLiveLogViewer_OptionsBuilderIsDefined_ServiceProviderReturnsConfiguredOptions()
    {
        // Arrange

        // Act
        _serviceCollection.AddLiveLogViewer((_, o) => o.BasePath = "/my/base/path");

        // Assert
        var options = _serviceCollection.BuildServiceProvider().GetService<LiveLogViewerOptions>();
        options.Should().BeEquivalentTo(new LiveLogViewerOptions { BasePath = "/my/base/path" });
    }
}