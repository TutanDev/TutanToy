﻿using Silk.NET.OpenGL;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Renderer.OpenGL;

internal class OpenGLTexture : ITexture
{
    public string Path { get; set; }

    private readonly uint _handle;
    private readonly GL _gl;

    private readonly byte[] _data;
    private readonly int _width;
    private readonly int _height;

    public unsafe OpenGLTexture(in string path, in Span<byte> data, int width, int height)
    {
        Path = path;

        _data = data.ToArray();
        _width = width;
        _height = height;

		_gl = OpenGLContext.GL;

		_handle = _gl.GenTexture();
		_gl.BindTexture(TextureTarget.Texture2D, _handle);

		fixed (byte* ptr = _data)
		{
			_gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)_width,
					(uint)_height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
		}

		SetParameters();
	}

    public void Bind(TextureUnit slot = TextureUnit.Texture0)
    {
        _gl.ActiveTexture(slot);
        _gl.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void Dispose() => _gl.DeleteTexture(_handle);


    // Private
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
