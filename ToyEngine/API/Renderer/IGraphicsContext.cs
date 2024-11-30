
using Silk.NET.GLFW;
using Silk.NET.Windowing;
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
			case RendererAPI.None:
				throw new NotImplementedException();
				break;
			case RendererAPI.OpenGL:
				result = new Implementation.OpenGL.GraphicsContext();
				result.Initialize(window);
				break;
			default:
				break;
		}

		return result;
	}
}
