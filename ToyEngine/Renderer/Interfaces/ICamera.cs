using System.Numerics;

namespace ToyEngine.Renderer.Interfaces;

public interface ICamera : IDisposable
{
    Matrix4x4 GetViewMatrix();
    Matrix4x4 GetProjectionMatrix();
    Matrix4x4 GetViewProjectionMatrix();
}
