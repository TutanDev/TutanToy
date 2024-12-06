using System.Numerics;
using ToyEditor.Importers;
using ToyEngine.Core;
using ToyEngine.Platform.Windows;
using ToyEngine.Render;
using ToyEngine.Renderer.API;
using ToyEngine.Renderer.Interfaces;
using Shader = ToyEngine.Renderer.API.Shader;

namespace ToyEditor;

public class ToyEditor : ToyApp
{
	ToyObject _toy;
	private Camera _camera;

	private IShader _shader;
	private AssetWatcher _shadersWatcher;

	Vector3 _cubePosition = new(0.0f, 0.0f, 0.0f);


	public override void Initialize()
	{
		base.Initialize();

		var modelImporter = new ModelImporter();
		var textureImporter = new TextureImporter();

		_shader = Shader.Create("simple.vert", "simple.frag");
		_toy = new ToyObject()
		{
			Transform = new() { Position = _cubePosition },
			Model = modelImporter.ImportModel("cube.model"),
			Shader = _shader,
			Texture = textureImporter.ImportImage("container.jpg"),
		};

		_camera = new Camera();
		_shadersWatcher = new(Directory.GetCurrentDirectory(), "*.vert", "*.frag");

		RenderCommand.SetClearColor(System.Drawing.Color.DarkSlateGray);
	}

	public override void OnUpdate(double deltaTime)
	{
		if (_shadersWatcher.IsDirty())
		{
			_shader.Dispose();
			_shader.Compile();
		}
	}

	public override void OnRender(double deltaTime)
	{
		RenderCommand.Clear();

		_renderer.BeginScene(_camera);
		_toy.Draw(_camera);
		_renderer.EndScene();
	}
}