
using Silk.NET.GLFW;
using System.Numerics;
using ToyEngine.Platform.API;
using ToyEngine.Platform.Windows;

namespace ToyEngine.Platform.Interfaces;

public interface IInput
{
	bool IsKeyPressed(KeyCode key);
	bool IsMouseButtonPressed(MouseCode button);
	public Vector2 GetMousePosition();
}

public static class Input
{
	public unsafe static IInput Create(WindowHandle* window) => new WindowsInput(window);
}