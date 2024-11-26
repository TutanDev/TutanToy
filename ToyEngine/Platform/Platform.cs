
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace ToyEngine.Platform;

public class Platform : IDisposable
{
	public IWindow Window { get; private set; }
	public IInputContext Input { get; private set; }


	public Platform()
	{
		var options = WindowOptions.Default;
		options.Size = new Vector2D<int>(800, 800);
		options.Title = "Playing with Silk.NET";
		Window = Silk.NET.Windowing.Window.Create(options);
		Window.Load += () => Input = Window.CreateInput();
	}

	public void Run() => Window.Run();

	public void Dispose() => Window.Dispose();
}
