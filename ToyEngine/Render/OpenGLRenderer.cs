
using ImGuiNET;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using System.Drawing;
using System.Numerics;
using ToyEngine.Platform;

namespace ToyEngine.Render;

internal class OpenGLRenderer : IRenderer
{
	private IWindow _window;
	private GL _gl;
	ImGuiController _imGui = null;

	private Camera _camera;

	private Shader _shader;
	private AssetWatcher _shadersWatcher;

	Vector3[] _cubePositions =
	{
		new( 0.0f,  0.0f,  0.0f),
		new( 2.0f,  5.0f, -15.0f),
		new(-1.5f, -2.2f, -2.5f),
		new(-3.8f, -2.0f, -12.3f),
		new( 2.4f, -0.4f, -3.5f),
		new(-1.7f,  3.0f, -7.5f),
		new( 1.3f, -2.0f, -2.5f),
		new( 1.5f,  2.0f, -2.5f),
		new( 1.5f,  0.2f, -1.5f),
		new(-1.3f,  1.0f, -1.5f)
	};


	public void Init(IWindow window, IInputContext input)
	{
		_window = window;


		_gl = _window.CreateOpenGL();

		_imGui = new ImGuiController(_gl, _window, input);

		_camera = new Camera(input);
		_shader = new(_gl, "simple.vert", "simple.frag");
		_shadersWatcher = new(Directory.GetCurrentDirectory(), "*.vert", "*.frag");


		_gl.ClearColor(Color.DarkSlateGray);
	}

	public void Resize(Vector2D<int> newSize)
	{
		_gl.Viewport(newSize);
	}

	public void BeginScene()
	{
		if (_shadersWatcher.IsDirty())
		{
			_shader.Dispose();
			_shader.Compile();
		}


		_gl.Enable(EnableCap.DepthTest);
		_gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

		
	}
	bool firstSubmit = true;
	public void Submit(ToyObject toy)
	{
		if(firstSubmit)
		{
			toy.Shader = _shader;
			toy.SetRenderContext(_gl);
			firstSubmit = false;
		}

		toy.Shader.Use();
		toy.Texture.Use(TextureUnit.Texture0);
		toy.Shader.SetUniform("uTexture", 0);
		toy.Shader.SetUniform("uView", _camera.View);
		toy.Shader.SetUniform("uProjection", _camera.GetProjectionMatrix(_window.Size));

		for (int i = 0; i < toy.Transforms.Length; i++)
		{
			var transform = toy.Transforms[i];
			transform.Position = _cubePositions[i];

			var angle = 20.0f * i;
			var axis = new Vector3(1, 0.5f, 0.2f);
			axis /= axis.Length();
			transform.Rotation = Quaternion.CreateFromAxisAngle(axis, angle);

			transform.Scale = 1.0f;

			toy.Shader.SetUniform("uModel", transform.ViewMatrix);
			toy.Model.Draw();
		}
	}

	public void EndScene()
	{
		
	}

	public void BeginUI(float deltaSeconds)
	{
		_imGui.Update(deltaSeconds);
	}

	public void EndUI()
	{
		_imGui.Render();
	}

	public void Destroy()
	{
		_imGui?.Dispose();
		_gl?.Dispose();
	}
}

public interface IRenderer
{
	public void Init(IWindow window, IInputContext input);
	public void Resize(Vector2D<int> newSize);


	void BeginScene();
	public void Submit(ToyObject toy);
	void EndScene();

	void BeginUI(float deltaSeconds);
	void EndUI();


	public void Destroy();
}
