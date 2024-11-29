

using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using ToyEngine.Platform.Interfaces;

namespace ToyEngine.Platform;

internal class Win32Platform : IWindowingPlatform
{
	private IWindow _window;


	public IWindow CreateWindow()
	{
		var options = WindowOptions.Default;
		options.Size = new Vector2D<int>(800, 800);
		options.Title = "Playing with Silk.NET";
		_window = Silk.NET.Windowing.Window.Create(options);

		return _window;
	}

	public IInputContext CreateInput()  => _window.CreateInput();
}

