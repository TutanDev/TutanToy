
using Silk.NET.OpenGL;

namespace ToyEditor.Render;

internal class VertexArrayObject<TVertexType, TIndexType> : IDisposable
        where TVertexType : unmanaged
        where TIndexType : unmanaged
{
    private uint ID;
    private GL _gl;

    public VertexArrayObject(GL gl, BufferObject<TVertexType> vbo, BufferObject<TIndexType> ebo)
    {
        _gl = gl;

        ID = _gl.GenVertexArray();
        _gl.BindVertexArray(ID);
        vbo.Bind();
        ebo.Bind();

        //_gl.BindVertexArray(0);
    }

    public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offSet)
    {
        _gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint)sizeof(TVertexType), (void*)(offSet * sizeof(TVertexType)));
        _gl.EnableVertexAttribArray(index);
    }

    public void Bind() => _gl.BindVertexArray(ID);

    public void Dispose() => _gl.DeleteVertexArray(ID);
}
