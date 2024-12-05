using Silk.NET.OpenGL;
using ToyEngine.Renderer.OpenGL;

namespace ToyEngine.Renderer.Interfaces;

public interface IVertexArray : IDisposable
{
    void Bind();
    void Unbind();

    void AddvertexBuffer(IVertexBuffer vertexBuffer);
    void SetIndexBuffer(IIndexBuffer indexBuffer);

    void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offSet);
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
                result = new OpenGLVertexArray(default);
                break;
        }

        return result;
    }
}
