
using Silk.NET.GLFW;

namespace ToyEngine.Renderer.Interfaces;

public interface IGraphicsContext
{
    unsafe void Initialize(Glfw glfw, WindowHandle* window);
    IRendererAPI CreateAPI();
    void SwapBuffers();
}