
using Silk.NET.OpenGL;
using ToyEngine.API.Renderer;

namespace ToyEngine.Implementation.OpenGL;

internal class VertexBuffer : IVertexBuffer
{
	private const BufferTargetARB _type = BufferTargetARB.ArrayBuffer;

	private GL _gl;
	private uint _handle;

    public VertexBuffer(GL gl)
    {
		_gl = gl;
		_handle = _gl.GenBuffer();
	}

	public VertexBuffer(GL gl, Span<float> vertices)
	{
		_gl = gl;
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

	public BufferLayout GetLayout()
	{
		throw new NotImplementedException();
	}

	public void SetLayout(BufferLayout layout)
	{
		throw new NotImplementedException();
	}


	public void Bind() => _gl.BindBuffer(_type, _handle);

	public void Unbind() => _gl.BindBuffer(_type, 0);

	public void Dispose() => _gl.DeleteBuffer(_handle);
}

internal class IndexBuffer : IIndexBuffer
{
	private const BufferTargetARB _type = BufferTargetARB.ElementArrayBuffer;

	private GL _gl;
	private uint _handle;

    public unsafe IndexBuffer(GL gl, Span<uint> indices)
    {
		_gl = gl;
		_handle = _gl.GenBuffer();
		_gl.BindBuffer(_type, _handle);

		fixed (void* d = indices)
		{
			_gl.BufferData(_type, (nuint)(indices.Length * sizeof(uint)), d, BufferUsageARB.StaticDraw);
		}
	}

    public int GetCount()
	{
		throw new NotImplementedException();
	}
	public void Bind() => _gl.BindBuffer(_type, _handle);

	public void Unbind() => _gl.BindBuffer(_type, 0);

	public void Dispose() => _gl.DeleteBuffer(_handle);
}
