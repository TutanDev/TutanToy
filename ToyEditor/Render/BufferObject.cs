
using Silk.NET.OpenGL;

namespace ToyEditor.Render;

internal class BufferObject<TDataType> : IDisposable where TDataType : unmanaged
{
    private uint ID;
    private BufferTargetARB _bufferType;

    private GL _gl;

    public unsafe BufferObject(GL gl, Span<TDataType> data, BufferTargetARB bufferType)
    {
        (_gl, _bufferType) = (gl, bufferType);

        ID = _gl.GenBuffer();
        _gl.BindBuffer(_bufferType, ID);

        fixed (void* d = data)
        {
            _gl.BufferData(bufferType, (nuint)(data.Length * sizeof(TDataType)), d, BufferUsageARB.StaticDraw);
        }

        //_gl.BindBuffer(_bufferType, 0);
    }

    public void Bind() => _gl.BindBuffer(_bufferType, ID);

    public void Dispose() => _gl.DeleteBuffer(ID);
}