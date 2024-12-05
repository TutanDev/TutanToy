using ToyEngine.Renderer.OpenGL;

namespace ToyEngine.Renderer.Interfaces;

public interface IBuffer<T> : IDisposable
{
    void Bind();
    void Unbind();
}

public interface IVertexBuffer : IBuffer<float>
{
    void SetData(Span<float> data);
    //void SetLayout(BufferLayout layout);
    //BufferLayout GetLayout();

}

public interface IIndexBuffer : IBuffer<int>
{
    //public int GetCount();
}

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
                result = new OpenGLVertexBuffer(default);
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
                result = new OpenGLVertexBuffer(default, vertices);
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
                result = new OpenGLIndexBuffer(default, indices);
                break;
        }

        return result;
    }
}
