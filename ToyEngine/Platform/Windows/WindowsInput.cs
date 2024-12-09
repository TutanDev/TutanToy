using Silk.NET.GLFW;
using System.Numerics;
using ToyEngine.Platform.API;
using ToyEngine.Platform.Interfaces;

namespace ToyEngine.Platform.Windows;

public unsafe class WindowsInput : IInput
{
	private readonly Glfw _glfw;
	private readonly WindowHandle* _window;

	public WindowsInput(WindowHandle* window)
    {
		_window = window;
		_glfw = Glfw.GetApi();
	}

	public bool IsKeyPressed(KeyCode key) => _glfw.GetKey(_window, (Keys)(int)key) > 0;

	public bool IsMouseButtonPressed(MouseCode button) => _glfw.GetMouseButton(_window, (int)button) > 0;

	public Vector2 GetMousePosition()
	{
		double x, y;
		_glfw.GetCursorPos(_window, out x, out y);
		return new((float)x, (float)y);
	}
}

