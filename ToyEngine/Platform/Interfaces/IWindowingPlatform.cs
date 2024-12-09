using ToyEngine.Events;
using ToyEngine.Platform.Windows;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Platform.Interfaces;

public record WindowProperties(string Title, int Width, int Height) { }

public interface IWindowingPlatform : IDisposable
{
    int Width { get; }
    int Height { get; }

    void OnUpdate();

    IGraphicsContext GetGraphicsContext();

    void SetEventCallback(Action<IEvent> callback);
}

public static class WindowingPlatform
{
    public static IWindowingPlatform Create(WindowProperties windowProperties)
        => new WindowsWindowing(windowProperties);
}
