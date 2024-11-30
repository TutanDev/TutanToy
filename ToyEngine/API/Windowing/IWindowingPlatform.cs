using ToyEngine.Implementation.Windows;

namespace ToyEngine.API.Windowing;

public record WindowProperties (string Title, int Width, int Height) {}

public interface IWindowingPlatform : IDisposable
{
    public int Width { get; }
    public int Height { get; }

    public void OnUpdate();
}

public static class WindowingPlatform
{
	public static IWindowingPlatform Create(WindowProperties windowProperties) 
        => new WindowsWindowing(windowProperties);
}
