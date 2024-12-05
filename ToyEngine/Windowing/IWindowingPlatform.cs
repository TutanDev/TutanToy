using ToyEngine.Renderer.Interfaces;
using ToyEngine.Windowing.Windows;

namespace ToyEngine.Windowing;

public record WindowProperties(string Title, int Width, int Height) { }

public interface IWindowingPlatform : IDisposable
{
    int Width { get; }
    int Height { get; }

    void OnUpdate();

    IGraphicsContext GetGraphicsContext();
}

public static class WindowingPlatform
{
    public static IWindowingPlatform Create(WindowProperties windowProperties)
        => new WindowsWindowing(windowProperties);
}
