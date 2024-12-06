
using System.Drawing;
using ToyEngine.Core;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Renderer.API;

public class RenderCommand
{
    private static IRendererAPI _api;

    public static void Initialize(IRendererAPI api)
    {
        _api = api;
        api.Initialize();
    }

    public static void SetViewPort(int x, int y, int width, int height) => _api.SetViewPort(x, y, width, height);
    public static void SetClearColor(Color color) => _api.SetClearColor(color);
    public static void Clear() => _api.Clear();
    public static void DrawArrays(IVertexArray vertexArray) => _api.DrawArrays(vertexArray);
    public static void DrawIndexed(IVertexArray vertexArray, int indexcount) => _api.DrawIndexed(vertexArray, indexcount);
}
