using System.Numerics;

namespace ToyEngine.Renderer.Interfaces;

public interface IShader : IDisposable
{
    void Compile();

    public void Use();

    void SetInt(string name, int value);
    void SetMat4(string name, Matrix4x4 value);
}


