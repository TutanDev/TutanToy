
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using ToyEngine.API.Renderer;

namespace ToyEngine.Implementation.OpenGL;

internal unsafe class GraphicsContext : IGraphicsContext
{
	WindowHandle* _window;
	Glfw _glfw;

	public unsafe void Initialize(WindowHandle* window)
	{
		_glfw = Glfw.GetApi();
		_glfw.MakeContextCurrent(window);
	}

	public void SwapBuffers()
	{
		_glfw.SwapBuffers(_window);
	}
}
