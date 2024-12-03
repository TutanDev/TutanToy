
using Silk.NET.GLFW;
using ToyEngine.API.Renderer;

namespace ToyEngine.Implementation.OpenGL;

internal unsafe class OpenGLContext : IGraphicsContext
{
	WindowHandle* _window;
	Glfw _glfw;

	public unsafe void Initialize(WindowHandle* window)
	{
		_window = window;
		_glfw = Glfw.GetApi();
		_glfw.MakeContextCurrent(window);
	}

	public void SwapBuffers()
	{
		_glfw.SwapBuffers(_window);
	}
}
