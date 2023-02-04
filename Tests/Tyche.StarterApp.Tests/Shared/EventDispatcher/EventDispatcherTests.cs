using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tyche.StarterApp.Shared.EventDispatcher;
using Xunit;

namespace Tyche.StarterApp.Tests.Shared.EventDispatcher;

public class EventDispatcherTests
{
    public class MockEvent {}
    
    public class MockWrongEvent {}

    private readonly Mock<IEventHandler<MockEvent>> _handler;
    
    private readonly Mock<IEventHandler<MockWrongEvent>> _wrongHandler;

    private readonly IEventDispatcher _eventDispatcher;

    public EventDispatcherTests()
    {
        var mockHandler = new Mock<IEventHandler<MockEvent>>();
        var mockWrongHandler = new Mock<IEventHandler<MockWrongEvent>>();
        _wrongHandler = mockWrongHandler;
        _handler = mockHandler;
        
        var services = new ServiceCollection()
            .AddEvents()
            .AddScoped<IEventHandler<MockEvent>>(e => mockHandler.Object)
            .AddScoped<IEventHandler<MockWrongEvent>>(e => mockWrongHandler.Object)
            .BuildServiceProvider();

        _eventDispatcher = services.GetService<IEventDispatcher>()!;
    }
    
    [Fact]
    public void Dispatch_SendsEvent_ToCorrectHandler()
    {
        // Arrange
        var @event = new MockEvent();
        
        // Act
        _eventDispatcher.Dispatch(@event);
        
        // Assert
        _handler.Verify(h => h.Handle(It.IsAny<MockEvent>()));
        _handler.VerifyNoOtherCalls();
        _wrongHandler.VerifyNoOtherCalls();
    }
}