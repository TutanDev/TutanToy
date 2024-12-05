

using Silk.NET.OpenGL;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Renderer.OpenGL;

internal class OpenGLVertexArray : IVertexArray
{
    private readonly GL _gl;
    private readonly uint _handle;

    public OpenGLVertexArray(GL gl)
    {
        _gl = gl;
        _handle = _gl.GenVertexArray();
    }


    public void AddvertexBuffer(IVertexBuffer vertexBuffer)
    {
        Bind();
        vertexBuffer.Bind();
        // TODO: handle layout
    }

    public void SetIndexBuffer(IIndexBuffer indexBuffer)
    {
        Bind();
        indexBuffer.Bind();
    }

    public void Bind() => _gl.BindVertexArray(_handle);

    public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offSet)
    {
        _gl.VertexAttribPointer(index, count, type, false, vertexSize * sizeof(float), (void*)(offSet * sizeof(float)));
        _gl.EnableVertexAttribArray(index);
    }

    public void Unbind() => _gl.BindVertexArray(0);

    public void Dispose() => _gl.DeleteVertexArray(_handle);
}
