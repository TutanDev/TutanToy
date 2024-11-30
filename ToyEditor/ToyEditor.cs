using System.Runtime.CompilerServices;
using ToyEditor.Importers;
using ToyEngine.Base;
using ToyEngine.Render;

namespace ToyEditor;

public class ToyEditor : ToyApp
{
	ToyObject _toy;

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

	public override void OnRender(double deltaTime)
	{
		//_renderer.Submit(_toy);
	}

	public override void OnUIRender(double deltaTime)
	{
		ImGuiNET.ImGui.ShowDemoWindow();
	}

}