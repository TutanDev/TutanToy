using Silk.NET.Input;
using Silk.NET.Windowing;

namespace ToyEngine.API.Windowing;

public interface IWindowingPlatform
{
    public IWindow CreateWindow();
    public IInputContext CreateInput();
}

