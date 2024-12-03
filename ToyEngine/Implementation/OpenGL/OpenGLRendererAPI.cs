using System.Drawing;
using ToyEngine.API.Renderer;

namespace ToyEngine.Implementation.OpenGL;

internal class OpenGLRendererAPI : IRendererAPI
{
	public void Initialize()
	{
		throw new NotImplementedException();
	}

	public void SetViewPort(int x, int y, int width, int height)
	{
		throw new NotImplementedException();
	}

	public void SetClearColor(Color color)
	{
		throw new NotImplementedException();
	}
	public void Clear()
	{
		throw new NotImplementedException();
	}


	public void DrawArrays(IVertexArray vertexArray)
	{
		throw new NotImplementedException();
	}

	public void DrawIndexed(IVertexArray vertexArray, int indexcount)
	{
		throw new NotImplementedException();
	}


	public void Dispose()
	{
		throw new NotImplementedException();
	}
}
