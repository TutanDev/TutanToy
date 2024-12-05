
using System.Drawing;
using ToyEngine.Renderer.OpenGL;

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

public class RendererAPI
{
    public static IRendererAPI Create(IGraphicsContext context)
    {
        if (context is OpenGLContext)
            return new OpenGLRendererAPI(OpenGLContext.GL);

        return default;
    }

    public static API GetAPI() => API.OpenGL;

    public enum API
    {
        None,
        OpenGL,
    }
}