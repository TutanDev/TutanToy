
using ToyEditor.Importers;
using ToyEngine;

namespace ToyEditor;

public class Program
{

	public static void Main(string[] args)
	{
		Directory.SetCurrentDirectory(Path.Combine(AppContext.BaseDirectory, "ToyAssets"));

		var modelImporter = new ModelImporter();
		var textureImporter = new TextureImporter();

		new ToyApp(modelImporter.ImportModel("cube.model"), textureImporter.ImportImage("container.jpg"));
	}
}