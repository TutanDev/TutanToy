using Silk.NET.OpenGL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToyEngine.Render;

public class Texture : IDisposable
{
	private uint ID;
	private GL _gl;

	public string Path { get; set; }

	private byte[] _data;
	private int _width;
	private int _height;

	public Texture(in string path, in byte[] data, int width, int height)
	{
		Path = path;

		_data = data;
		_width = width;
		_height = height;
	}

	public unsafe void SetRenderContext(GL gl)
	{
		_gl = gl;

		ID = _gl.GenTexture();
		_gl.BindTexture(TextureTarget.Texture2D, ID);

		fixed (byte* ptr = _data)
		{
			_gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)_width,
					(uint)_height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
		}

		SetParameters();

		_gl.BindTexture(TextureTarget.Texture2D, 0);
	}

	public unsafe Texture(GL gl, Span<byte> data, uint width, uint height)
	{
		_gl = gl;

		ID = _gl.GenTexture();
		_gl.BindTexture(TextureTarget.Texture2D, ID);

		fixed (void* d = &data[0])
		{
			_gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, d);
		}

		SetParameters();

		_gl.BindTexture(TextureTarget.Texture2D, 0);
	}

	public void Use(TextureUnit slot)
	{
		_gl.ActiveTexture(slot);
		_gl.BindTexture(TextureTarget.Texture2D, ID);
	}

	public void Dispose() => _gl.DeleteTexture(ID);

	private void SetParameters()
	{
		_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.ClampToEdge);
		_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.ClampToEdge);
		_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.LinearMipmapLinear);
		_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);
		_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
		_gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);

		_gl.GenerateMipmap(TextureTarget.Texture2D);
	}
}
