using Silk.NET.OpenGL;
using System.Numerics;
using ToyEngine.Renderer.OpenGL;

namespace ToyEngine.Renderer.Interfaces;

public interface IShader : IDisposable
{
    void Compile();

    public void Use();

    void SetInt(string name, int value);
    void SetMat4(string name, Matrix4x4 value);
}

public class Shader
{
    public static IShader Create(string vertexPath, string fragmentPath)
    {
        IShader result = default;

        switch (RendererAPI.GetAPI())
        {
            case RendererAPI.API.None:
                throw new NotImplementedException();
            case RendererAPI.API.OpenGL:
                result = new OpenGLShader(vertexPath, fragmentPath);
                break;
        }

        return result;
    }
}
