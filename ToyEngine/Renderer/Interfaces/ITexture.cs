using Silk.NET.OpenGL;
using ToyEngine.Renderer.OpenGL;

namespace ToyEngine.Renderer.Interfaces;

public interface ITexture : IDisposable
{
    public string Path { get; }

    void SetRenderContext(GL gl);
    void Bind(TextureUnit slot = TextureUnit.Texture0);
}

public class Texture
{
    public static ITexture Create(string path, Span<byte> data, int width, int height)
    {
        ITexture result = default;

        switch (RendererAPI.GetAPI())
        {
            case RendererAPI.API.None:
                throw new NotImplementedException();
            case RendererAPI.API.OpenGL:
                result = new OpenGLTexture(path, data, width, height);
                break;
        }

        return result;
    }
}