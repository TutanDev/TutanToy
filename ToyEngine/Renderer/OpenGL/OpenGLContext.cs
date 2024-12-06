
using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using ToyEngine.Renderer.API;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Renderer.OpenGL;

internal unsafe class OpenGLContext : IGraphicsContext
{
    WindowHandle* _window;
    Glfw _glfw;
    public static GL GL;

    public unsafe void Initialize(Glfw glfw, WindowHandle* window)
    {
        _window = window;
        _glfw = glfw;

        _glfw.MakeContextCurrent(window);

    }
    public IRendererAPI CreateAPI()
    {
        GL = GL.GetApi(new GlfwContext(_glfw, _window));
        return RendererAPI.Create(this);
    }

    public void SwapBuffers()
    {
        _glfw.SwapBuffers(_window);
    }

}
