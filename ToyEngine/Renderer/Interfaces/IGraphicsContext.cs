
using Silk.NET.GLFW;
using ToyEngine.Renderer.OpenGL;
using ToyEngine.Utils;

namespace ToyEngine.Renderer.Interfaces;

public interface IGraphicsContext
{
    unsafe void Initialize(Glfw glfw, WindowHandle* window);
    IRendererAPI CreateAPI();
    void SwapBuffers();
}

public class GraphicsContext
{
    public static unsafe IGraphicsContext Create(Glfw glfw, WindowHandle* window)
    {
        IGraphicsContext result = default;

        switch (RendererAPI.GetAPI())
        {
            case RendererAPI.API.None:
                throw new NotImplementedException();
            case RendererAPI.API.OpenGL:
                result = new OpenGLContext();
                result.Initialize(glfw, window);
                break;
        }

        return result;
    }
}
