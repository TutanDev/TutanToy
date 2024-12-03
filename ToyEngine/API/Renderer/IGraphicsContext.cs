
using Silk.NET.GLFW;
using ToyEngine.Utils;

namespace ToyEngine.API.Renderer;

public interface IGraphicsContext
{
	unsafe void Initialize(WindowHandle* window);
	void SwapBuffers();
}

public class GraphicsContext
{
	public static unsafe IGraphicsContext Create(WindowHandle* window)
	{
		IGraphicsContext result = default;

		switch (RuntimePlatform.GetRendererAPI())
		{
			case Utils.RendererAPI.None:
				throw new NotImplementedException();
			case Utils.RendererAPI.OpenGL:
				result = new Implementation.OpenGL.OpenGLContext();
				result.Initialize(window);
				break;
		}

		return result;
	}
}
