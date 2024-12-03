
using System.Drawing;
using ToyEngine.Implementation.OpenGL;
using ToyEngine.Utils;

namespace ToyEngine.API.Renderer;

public interface IRendererAPI : IDisposable
{
	void Initialize();

	void SetViewPort(int x, int y, int width, int height);
	void SetClearColor(Color color);
	void Clear();

	void DrawArrays(IVertexArray vertexArray);
	void DrawIndexed(IVertexArray vertexArray, int indexcount);
}

public class RendererAPI
{
	public IRendererAPI Create()
	{
		IRendererAPI result = default;

		switch (RuntimePlatform.GetRendererAPI())
		{
			case Utils.RendererAPI.None:
				throw new NotImplementedException();
			case Utils.RendererAPI.OpenGL:
				result = new OpenGLRendererAPI();
				break;
		}

		return result;
	}
}
