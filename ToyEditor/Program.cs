
using ToyEditor.Importers;
using ToyEngine;
using ToyEngine.Render;

namespace ToyEditor;

public class Program
{

	public static void Main(string[] args)
	{
		Directory.SetCurrentDirectory(Path.Combine(AppContext.BaseDirectory, "ToyAssets"));

		var modelImporter = new ModelImporter();
		var textureImporter = new TextureImporter();

		ToyObject toy = new ToyObject()
		{
			Transforms = new Transform[10],
			Model = modelImporter.ImportModel("cube.model"),
			Texture = textureImporter.ImportImage("container.jpg"),
		};

		ToyAppBuilder.Configure<ToyEditor>()
			.UseToy(toy)
			.Start();
	}
}

public class ToyEditor : ToyApp
{
	public ToyEditor() : base()
	{

	}


}