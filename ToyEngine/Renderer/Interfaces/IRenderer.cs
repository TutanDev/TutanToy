
using System.Numerics;

namespace ToyEngine.Renderer.Interfaces;

public struct SceneData
{
    public Matrix4x4 ViewMatrix;
    public Matrix4x4 ProjectionMatrix;
}

public interface IRenderer : IDisposable
{
    void Initialize(IGraphicsContext context);
    void Resize(Vector2 newSize);

    void BeginScene(ICamera camera);
    void EndScene();

    void Submit(IShader shader, IVertexArray vertexArray, Matrix4x4 transform);

    IRendererAPI GetAPI();
}
