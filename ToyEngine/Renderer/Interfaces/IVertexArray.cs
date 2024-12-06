using Silk.NET.OpenGL;

namespace ToyEngine.Renderer.Interfaces;

public interface IVertexArray : IDisposable
{
    void Bind();
    void Unbind();

    void AddvertexBuffer(IVertexBuffer vertexBuffer);
    void SetIndexBuffer(IIndexBuffer indexBuffer);

    void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offSet);
}


