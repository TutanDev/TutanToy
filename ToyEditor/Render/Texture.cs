using Silk.NET.Assimp;
using Silk.NET.OpenGL;
using StbImageSharp;

namespace ToyEditor.Render;

public class Texture : IDisposable
{
    public string Path { get; set; }
    public TextureType Type { get; }

    private uint ID;

    private readonly GL _gl;

    public unsafe Texture(in GL gl, in string path, TextureType type = TextureType.None)
    {
        _gl = gl;
        Path = path;
        Type = type;

        ID = _gl.GenTexture();
        _gl.BindTexture(TextureTarget.Texture2D, ID);

        // Change to ImageSharp?
        ImageResult result = ImageResult.FromMemory(System.IO.File.ReadAllBytes(path), ColorComponents.RedGreenBlueAlpha);

        fixed (byte* ptr = result.Data)
        {
            _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)result.Width,
                    (uint)result.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
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
