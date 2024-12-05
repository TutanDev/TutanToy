

using System.Diagnostics;

using Silk.NET.GLFW;
using ToyEngine.Renderer.Interfaces;
using ToyEngine.Utils;
using ToyEngine.Windowing;

namespace ToyEngine.Windowing.Windows;

internal unsafe class WindowsWindowing : IWindowingPlatform
{
    private static int s_windowCount = 0;

    private static GlfwCallbacks.ErrorCallback s_errorCallback =>
        (e, description) => Debug.WriteLine($"GLFW ERROR! code: {e}, description: {description}");


    private WindowProperties _properties;

    private Glfw _glfw;
    private WindowHandle* _handle;

    private IGraphicsContext _graphicsContext;


    public int Width => _properties.Width;
    public int Height => _properties.Height;

    public WindowsWindowing()
    {
        _properties = new WindowProperties("Toy Engine", 1600, 900);
        _glfw = Glfw.GetApi();
        Initialize(_properties);
    }

    public WindowsWindowing(WindowProperties properties)
    {
        _properties = properties;
        _glfw = Glfw.GetApi();
        Initialize(properties);
    }

    public void OnUpdate()
    {
        _glfw.PollEvents();
        _graphicsContext.SwapBuffers();
    }

    public IGraphicsContext GetGraphicsContext() => _graphicsContext;

    public void Dispose()
    {
        _glfw.DestroyWindow(_handle);
        --s_windowCount;

        if (s_windowCount == 0)
        {
            _glfw.Terminate();
        }
    }

    // Private
    private unsafe void Initialize(WindowProperties properties)
    {
        if (s_windowCount == 0)
        {
            bool init = _glfw.Init();
            if (!init)
                return;

            _glfw.SetErrorCallback(s_errorCallback);
        }

        if (RendererAPI.GetAPI() == RendererAPI.API.OpenGL)
        {
            _glfw.WindowHint(WindowHintBool.OpenGLDebugContext, true);
        }

        _handle = _glfw.CreateWindow(properties.Width, properties.Height, properties.Title, null, null);
        ++s_windowCount;

        _graphicsContext = GraphicsContext.Create(_glfw, _handle);

        //_glfw.MakeContextCurrent(_handle);
        SetVSync(true);

        _glfw.SetWindowSizeCallback(_handle, WindowSizeCallback);
        _glfw.SetWindowCloseCallback(_handle, WindowCloseCallback);
        _glfw.SetKeyCallback(_handle, KeyCallback);
        _glfw.SetCharCallback(_handle, CharCallback);
        _glfw.SetMouseButtonCallback(_handle, MouseButtonCallback);
        _glfw.SetScrollCallback(_handle, ScrollCallback);
        _glfw.SetCursorPosCallback(_handle, CursorPosCallback);
    }

    void SetVSync(bool enabled)
    {
        if (enabled)
            _glfw.SwapInterval(1);
        else
            _glfw.SwapInterval(0);
    }

    #region Callbacks
    private void WindowSizeCallback(WindowHandle* window, int width, int height)
    {
        _properties = _properties with { Height = height, Width = width };

        // Raise ResizeEvent
        Debug.WriteLine("WindowSizeCallback");
    }

    private void WindowCloseCallback(WindowHandle* window)
    {
        // Raise WindowCloseCallback
        Debug.WriteLine("WindowCloseCallback");
    }

    private void KeyCallback(WindowHandle* window, Keys key, int scanCode, InputAction action, KeyModifiers mods)
    {
        // Raise KeyCallback
        Debug.WriteLine("KeyCallback");
    }

    private void CharCallback(WindowHandle* window, uint codepoint)
    {
        // Raise CharCallback
        Debug.WriteLine("CharCallback");
    }

    private void MouseButtonCallback(WindowHandle* window, MouseButton button, InputAction action, KeyModifiers mods)
    {
        // Raise MouseButtonCallback
        Debug.WriteLine("MouseButtonCallback");
    }

    private void ScrollCallback(WindowHandle* window, double offsetX, double offsetY)
    {
        // Raise ScrollCallback
        Debug.WriteLine("ScrollCallback");
    }

    private void CursorPosCallback(WindowHandle* window, double x, double y)
    {
        // Raise CursorPosCallback
        Debug.WriteLine("CursorPosCallback");
    }
    #endregion Callbacks
}

