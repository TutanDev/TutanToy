using Silk.NET.Assimp;
using StbImageSharp;

using Texture = ToyEngine.Render.Texture;


namespace ToyEditor.Importers;

internal class TextureImporter
{
	public string Path { get; set; }
	public TextureType Type { get; private set; }


	public Texture ImportImage(in string path, TextureType type = TextureType.None)
	{
		// Change to ImageSharp?
		ImageResult result = ImageResult.FromMemory(System.IO.File.ReadAllBytes(path), ColorComponents.RedGreenBlueAlpha);
		return new Texture(path, result.Data, result.Width, result.Height);
	}

}
