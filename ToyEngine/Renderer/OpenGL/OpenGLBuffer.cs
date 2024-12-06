
using Silk.NET.OpenGL;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Renderer.OpenGL;

internal class OpenGLVertexBuffer : IVertexBuffer
{
    private const BufferTargetARB _type = BufferTargetARB.ArrayBuffer;

    private readonly GL _gl;
    private readonly uint _handle;

    public OpenGLVertexBuffer()
    {
		_gl = OpenGLContext.GL;
		_handle = _gl.GenBuffer();
    }

    public OpenGLVertexBuffer(Span<float> vertices)
    {
		_gl = OpenGLContext.GL;
		_handle = _gl.GenBuffer();
        SetData(vertices);
    }

    public unsafe void SetData(Span<float> data)
    {
        _gl.BindBuffer(_type, _handle);

        fixed (void* d = data)
        {
            _gl.BufferData(_type, (nuint)(data.Length * sizeof(float)), d, BufferUsageARB.StaticDraw);
        }
    }


    public void Bind() => _gl.BindBuffer(_type, _handle);

    public void Unbind() => _gl.BindBuffer(_type, 0);

    public void Dispose() => _gl.DeleteBuffer(_handle);
}

internal class OpenGLIndexBuffer : IIndexBuffer
{
    private const BufferTargetARB _type = BufferTargetARB.ElementArrayBuffer;

    private readonly GL _gl;
    private readonly uint _handle;

    public unsafe OpenGLIndexBuffer(Span<uint> indices)
    {
        _gl = OpenGLContext.GL;
        _handle = _gl.GenBuffer();
        _gl.BindBuffer(_type, _handle);

        fixed (void* d = indices)
        {
            _gl.BufferData(_type, (nuint)(indices.Length * sizeof(uint)), d, BufferUsageARB.StaticDraw);
        }
    }

    public void Bind() => _gl.BindBuffer(_type, _handle);

    public void Unbind() => _gl.BindBuffer(_type, 0);

    public void Dispose() => _gl.DeleteBuffer(_handle);
}
