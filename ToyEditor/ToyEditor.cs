using Silk.NET.OpenGL;
using System.Numerics;
using System.Runtime.CompilerServices;
using ToyEditor.Importers;
using ToyEngine.Core;
using ToyEngine.Render;
using ToyEngine.Renderer.API;
using ToyEngine.Renderer.Interfaces;
using ToyEngine.Utils;

namespace ToyEditor;

public class ToyEditor : ToyApp
{
	ToyObject _toy;
	private Camera _camera;

	private IShader _shader;
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


	public ToyEditor() : base()
	{
		var modelImporter = new ModelImporter();
		var textureImporter = new TextureImporter();

		_toy = new ToyObject()
		{
			Transforms = new Transform[10],
			Model = modelImporter.ImportModel("cube.model"),
			Texture = textureImporter.ImportImage("container.jpg"),
		};
	}


	public override void Initialize()
	{
		base.Initialize();

		_camera = new Camera();
		_shader = ToyEngine.Renderer.Interfaces.Shader.Create("simple.vert", "simple.frag");
		_shadersWatcher = new(Directory.GetCurrentDirectory(), "*.vert", "*.frag");
	}

	public override void OnUpdate(double deltaTime)
	{
		if (_shadersWatcher.IsDirty())
		{
			_shader.Dispose();
			_shader.Compile();
		}

		for (int i = 0; i < _toy.Transforms.Length; i++)
		{
			var transform = _toy.Transforms[i];
			transform.Position = _cubePositions[i];

			var angle = 20.0f * i;
			var axis = new Vector3(1, 0.5f, 0.2f);
			axis /= axis.Length();
			transform.Rotation = Quaternion.CreateFromAxisAngle(axis, angle);

			transform.Scale = 1.0f;

			_toy.Shader.SetMat4("uModel", transform.ViewMatrix);
			_toy.Model.Draw();
		}
	}

	public override void OnRender(double deltaTime)
	{
		RenderCommand.Clear();


	}
}