using ToyEngine.Base;
using ToyEngine.Implementation;
using ToyEngine.Implementation.OpenGL;

namespace ToyEngine.API.Renderer;

public interface IBuffer<T> : IDisposable 
{
	void Bind();
	void Unbind();
}

public interface IVertexBuffer : IBuffer<float>
{
	void SetData(Span<float> data);
	void SetLayout(BufferLayout layout);
	BufferLayout GetLayout();

}

public interface IIndexBuffer : IBuffer<int>
{
	public int GetCount();
}

public struct BufferLayout
{

}

public static class VertexBuffer
{
	public static IVertexBuffer Create()
	{
		IVertexBuffer result = default;

		switch (RuntimePlatform.GetRendererAPI())
		{
			case RendererAPI.None:
				throw new NotImplementedException();
				break;
			case RendererAPI.OpenGL:
				result = new Implementation.OpenGL.VertexBuffer(default);
				break;
			default:
				break;
		}

		return result;
	}
	public static IVertexBuffer Create(Span<float> vertices)
	{
		IVertexBuffer result = default;

		switch (RuntimePlatform.GetRendererAPI())
		{
			case RendererAPI.None:
				throw new NotImplementedException();
				break;
			case RendererAPI.OpenGL:
				result = new Implementation.OpenGL.VertexBuffer(default, vertices);
				break;
			default:
				break;
		}

		return result;
	}
}

public static class IndexBuffer
{
	public static IIndexBuffer Create(Span<uint> indices) 
	{
		IIndexBuffer result = default;

		switch (RuntimePlatform.GetRendererAPI())
		{
			case RendererAPI.None:
				throw new NotImplementedException();
				break;
			case RendererAPI.OpenGL:
				result = new Implementation.OpenGL.IndexBuffer(default, indices);
				break;
			default:
				break;
		}

		return result;
	}
}
