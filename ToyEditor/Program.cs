using System.Drawing;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System.Numerics;

using ToyEditor.Render;
using ToyEditor.Platform;
using System.Reflection;

namespace ToyEditor;

public class Program
{
	private static Platform.Platform _platform;
	private static GL _gl;

	private static Model _model;
	private static Render.Shader _shader;
	private static Render.Texture _texture;
	private static Camera _camera;

	private static AssetWatcher _shadersWatcher;


	static Vector3[] _cubePositions =
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

	public static void Main(string[] args)
	{
		Directory.SetCurrentDirectory(Path.Combine(AppContext.BaseDirectory, "Assets"));
		_shadersWatcher = new(Directory.GetCurrentDirectory(), "*.vert", "*.frag");

		_platform = new Platform.Platform();
		var window = _platform.Window;
		window.Load += OnLoad;
		window.Update += OnUpdate;
		window.Render += OnRender;
		window.FramebufferResize += OnFramebufferResize;
		window.Closing += OnClose;
		// Loop
		_platform.Run();

		_platform.Dispose();
	}



	private static unsafe void OnLoad()
	{
		var window = _platform.Window;
		_gl = window.CreateOpenGL();
		_gl.ClearColor(Color.DarkSlateGray);

		_camera = new Camera(_platform.Input);
		_model = new(_gl, "cube.model");
		_shader = new(_gl, "simple.vert", "simple.frag");
		_texture = new(_gl, "container.jpg");
	}

	private static void OnUpdate(double dt)
	{
		_camera.Update((float)dt);
	}

	private static unsafe void OnRender(double deltaTime)
	{
		var window = _platform.Window;
		if (_shadersWatcher.IsDirty())
		{
			_shader.Dispose();
			_shader.Compile();
		}

		_gl.Enable(EnableCap.DepthTest);
		_gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

		_shader.Use();
		_texture.Use(TextureUnit.Texture0);
		_shader.SetUniform("uTexture", 0);
		_shader.SetUniform("uView", _camera.View);
		_shader.SetUniform("uProjection", _camera.GetProjectionMatrix(window.Size));

		for (int i = 0; i < _cubePositions.Count(); i++)
		{
			var model = new Transform();
			model.Position = _cubePositions[i];

			var angle = 20.0f * i;
			var axis = new Vector3(1, 0.5f, 0.2f);
			axis /= axis.Length();
			model.Rotation = Quaternion.CreateFromAxisAngle(axis, angle);

			_shader.SetUniform("uModel", model.ViewMatrix);
			_model.Draw();
		}
	}

	private static void OnFramebufferResize(Vector2D<int> newSize) => _gl.Viewport(newSize);


	private static void OnClose()
	{
		_model.Dispose();
		_shader.Dispose();
		_texture.Dispose();
	}
}