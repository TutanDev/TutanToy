using Silk.NET.OpenGL;

namespace ToyEngine.Renderer.Interfaces;

public interface ITexture : IDisposable
{
    public string Path { get; }

    void Bind(TextureUnit slot = TextureUnit.Texture0);
}