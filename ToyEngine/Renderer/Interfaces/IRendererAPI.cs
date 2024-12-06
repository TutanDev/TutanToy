
using System.Drawing;

namespace ToyEngine.Renderer.Interfaces;

public interface IRendererAPI
{
    void Initialize();

    void SetViewPort(int x, int y, int width, int height);
    void SetClearColor(Color color);
    void Clear();

    void DrawArrays(IVertexArray vertexArray);
    void DrawIndexed(IVertexArray vertexArray, int indexcount);
}

