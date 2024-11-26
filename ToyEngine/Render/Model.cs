using Silk.NET.OpenGL;

namespace ToyEngine.Render;

public class Model : IDisposable
{
	private readonly GL _gl;

	private readonly List<Mesh> Meshes;
	private readonly List<Texture> _texturesLoaded;

	public Model(List<Mesh> meshes, List<Texture> texturesLoaded, bool gamma = false)
	{
		Meshes = meshes;
		_texturesLoaded = texturesLoaded;
	}
	internal void SetRenderContext(GL gl)
	{
		foreach (var mesh in Meshes)
		{
			mesh.SetRenderContext(gl);
		}
	}

	public void Draw()
	{
		foreach (var mesh in Meshes)
		{
			mesh.Draw();
		}
	}

	public void Dispose()
	{
		foreach (var mesh in Meshes)
		{
			mesh.Dispose();
		}
	}
}