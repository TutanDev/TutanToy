
using Silk.NET.OpenGL;

namespace ToyEngine.Render;

internal class BufferObject<TDataType> : IDisposable where TDataType : unmanaged
{
	private uint ID;
	private BufferTargetARB _bufferType;

	private GL _gl;

	public unsafe BufferObject(GL gl, Span<TDataType> data, BufferTargetARB bufferType)
	{
		(_gl, _bufferType) = (gl, bufferType);

		//Clear existing error code.
		GLEnum error;
		do error = _gl.GetError();
		while (error != GLEnum.NoError);

		ID = _gl.GenBuffer();
		_gl.BindBuffer(_bufferType, ID);

		GlErrorException.ThrowIfError(gl);
		fixed (void* d = data)
		{
			_gl.BufferData(bufferType, (nuint)(data.Length * sizeof(TDataType)), d, BufferUsageARB.StaticDraw);
		}
		GlErrorException.ThrowIfError(gl);
	}

	public void Bind() => _gl.BindBuffer(_bufferType, ID);

	public void Dispose() => _gl.DeleteBuffer(ID);
}