
using System.Numerics;

namespace ToyEngine.API.Renderer;

public interface ICamera { }

public struct SceneData
{
	Matrix4x4 ViewProjectionMatrix;
}

public interface IRenderer : IDisposable
{
	void Initialize();
	void Resize(Vector2 newSize);

	void BeginScene(ICamera camera);
	void EndScene();

	void Submit();
}
