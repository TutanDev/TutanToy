
using Silk.NET.GLFW;
using ToyEngine.Renderer.Interfaces;
using ToyEngine.Renderer.OpenGL;

namespace ToyEngine.Renderer.API;

public static class VertexBuffer
{
	public static IVertexBuffer Create()
	{
		IVertexBuffer result = default;

		switch (RendererAPI.GetAPI())
		{
			case RendererAPI.API.None:
				throw new NotImplementedException();
			case RendererAPI.API.OpenGL:
				result = new OpenGLVertexBuffer();
				break;
		}

		return result;
	}
	public static IVertexBuffer Create(Span<float> vertices)
	{
		IVertexBuffer result = default;

		switch (RendererAPI.GetAPI())
		{
			case RendererAPI.API.None:
				throw new NotImplementedException();
			case RendererAPI.API.OpenGL:
				result = new OpenGLVertexBuffer(vertices);
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

		switch (RendererAPI.GetAPI())
		{
			case RendererAPI.API.None:
				throw new NotImplementedException();
			case RendererAPI.API.OpenGL:
				result = new OpenGLIndexBuffer(indices);
				break;
		}

		return result;
	}
}

public class GraphicsContext
{
	public static unsafe IGraphicsContext Create(Glfw glfw, WindowHandle* window)
	{
		IGraphicsContext result = default;

		switch (RendererAPI.GetAPI())
		{
			case RendererAPI.API.None:
				throw new NotImplementedException();
			case RendererAPI.API.OpenGL:
				result = new OpenGLContext();
				result.Initialize(glfw, window);
				break;
		}

		return result;
	}
}

public class RendererAPI
{
	public static IRendererAPI Create(IGraphicsContext context)
	{
		if (context is OpenGLContext)
			return new OpenGLRendererAPI(OpenGLContext.GL);

		return default;
	}

	public static API GetAPI() => API.OpenGL;

	public enum API
	{
		None,
		OpenGL,
	}
}

public class Shader
{
	public static IShader Create(string vertexPath, string fragmentPath)
	{
		IShader result = default;

		switch (RendererAPI.GetAPI())
		{
			case RendererAPI.API.None:
				throw new NotImplementedException();
			case RendererAPI.API.OpenGL:
				result = new OpenGLShader(vertexPath, fragmentPath);
				break;
		}

		return result;
	}
}

public class Texture
{
	public static ITexture Create(string path, Span<byte> data, int width, int height)
	{
		ITexture result = default;

		switch (RendererAPI.GetAPI())
		{
			case RendererAPI.API.None:
				throw new NotImplementedException();
			case RendererAPI.API.OpenGL:
				result = new OpenGLTexture(path, data, width, height);
				break;
		}

		return result;
	}
}

public class VertexArray
{
	public static IVertexArray Create()
	{
		IVertexArray result = default;

		switch (RendererAPI.GetAPI())
		{
			case RendererAPI.API.None:
				throw new NotImplementedException();
			case RendererAPI.API.OpenGL:
				result = new OpenGLVertexArray();
				break;
		}

		return result;
	}
}