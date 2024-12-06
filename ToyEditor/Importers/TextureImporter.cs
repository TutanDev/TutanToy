using Silk.NET.Assimp;
using StbImageSharp;
using ToyEngine.Renderer.Interfaces;
using Texture = ToyEngine.Renderer.API.Texture;


namespace ToyEditor.Importers;

internal class TextureImporter
{
	public string Path { get; set; }
	public TextureType Type { get; private set; }


	public ITexture ImportImage(in string path, TextureType type = TextureType.None)
	{
		// Change to ImageSharp?
		ImageResult result = ImageResult.FromMemory(System.IO.File.ReadAllBytes(path), ColorComponents.RedGreenBlueAlpha);
		return Texture.Create(path, result.Data, result.Width, result.Height);
	}

}
