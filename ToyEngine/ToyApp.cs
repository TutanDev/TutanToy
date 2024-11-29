using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Drawing;
using System.Numerics;
using ToyEngine.Platform;
using ToyEngine.Render;

using Camera = ToyEngine.Render.Camera;
using Shader = ToyEngine.Render.Shader;
using Texture = ToyEngine.Render.Texture;

namespace ToyEngine;

public class ToyApp : IGameLoop
{
	private Platform.Platform _platform;
	private GL _gl;

	private Shader _shader;
	private Camera _camera;

	private AssetWatcher _shadersWatcher;

	private ToyObject _toy;

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

	public ToyApp() { }


	public void Init()
	{
		var window = _platform.Window;
		_gl = window.CreateOpenGL();
		_gl.ClearColor(Color.DarkSlateGray);

		_camera = new Camera(_platform.Input);
		_shader = new(_gl, "simple.vert", "simple.frag");

		_toy.Shader = _shader;
		_toy.SetRenderContext(_gl);
	}

	public void Tick(double dt)
	{
		_camera.Update((float)dt);
	}

	public void Render(double dt)
	{
		var window = _platform.Window;
		if (_shadersWatcher.IsDirty())
		{
			_shader.Dispose();
			_shader.Compile();
		}

		_gl.Enable(EnableCap.DepthTest);
		_gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

		_toy.Shader.Use();
		_toy.Texture.Use(TextureUnit.Texture0);
		_toy.Shader.SetUniform("uTexture", 0);
		_toy.Shader.SetUniform("uView", _camera.View);
		_toy.Shader.SetUniform("uProjection", _camera.GetProjectionMatrix(window.Size));

		for (int i = 0; i < _toy.Transforms.Length; i++)
		{
			var transform = _toy.Transforms[i];
			transform.Position = _cubePositions[i];

			var angle = 20.0f * i;
			var axis = new Vector3(1, 0.5f, 0.2f);
			axis /= axis.Length();
			transform.Rotation = Quaternion.CreateFromAxisAngle(axis, angle);

			transform.Scale = 1.0f;

			_toy.Shader.SetUniform("uModel", transform.ViewMatrix);
			_toy.Model.Draw();
		}
	}

	public void Closing()
	{
		_toy.Dispose();
	}

	private void OnFramebufferResize(Vector2D<int> newSize) => _gl.Viewport(newSize);

	public void SetToy(ToyObject toy) => _toy = toy;

	public virtual void RegisterServices() 
	{
		_shadersWatcher = new(Directory.GetCurrentDirectory(), "*.vert", "*.frag");

		_platform = new Platform.Platform();
		var window = _platform.Window;
		window.Load += Init;
		window.Update += Tick;
		window.Render += Render;
		window.FramebufferResize += OnFramebufferResize;
		window.Closing += Closing;
	}

	public virtual void Initialize() 
	{
		// Loop
		_platform.Run();

		_platform.Dispose();
	}
	public virtual void OnFrameworkInitializationCompleted() { }
}

public interface IGameLoop
{
	void Init();
	void Tick(double dt);
	void Render(double dt);
	void Closing();
}
