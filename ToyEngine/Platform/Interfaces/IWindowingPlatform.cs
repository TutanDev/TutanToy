using Silk.NET.Input;
using Silk.NET.Windowing;

namespace ToyEngine.Platform.Interfaces;

public interface IWindowingPlatform
{
    public IWindow CreateWindow();
	public IInputContext CreateInput();
}

