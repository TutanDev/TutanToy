using System.Numerics;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Renderer.API;

public class Renderer : IRenderer
{
    private IRendererAPI _api;

    private SceneData _sceneData;


    public void Initialize(IGraphicsContext context)
    {
        _api = context.CreateAPI();
        RenderCommand.Initialize(_api);
    }

    public void Resize(Vector2 newSize) => RenderCommand.SetViewPort(0, 0, (int)newSize.X, (int)newSize.Y);

    public void BeginScene(ICamera camera)
    {
        _sceneData.ViewMatrix = camera.GetViewMatrix();
        _sceneData.ProjectionMatrix = camera.GetProjectionMatrix();
        _sceneData.ViewProjectionMatrix = camera.GetViewProjectionMatrix();
    }

    public void Submit(IShader shader, IVertexArray vertexArray, Matrix4x4 transform)
    {
        shader.Use();
        shader.SetMat4("uView", transform);
        shader.SetMat4("uProjection", transform);
        shader.SetMat4("uModel", transform);

        RenderCommand.DrawArrays(vertexArray);
    }

    public void EndScene() { }

    public void Dispose() { }

    public IRendererAPI GetAPI() => _api;
}
