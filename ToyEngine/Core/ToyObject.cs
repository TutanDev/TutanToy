using Silk.NET.OpenGL;
using ToyEngine.Render;
using ToyEngine.Renderer.Interfaces;


namespace ToyEngine.Core
{
    public class ToyObject : IDisposable
    {
        public Transform Transform { get; set; }
        public Model Model { get; set; }
        public IShader Shader { get; set; }
        public ITexture Texture { get; set; }

        public void Draw(ICamera camera)
        {
            Shader.Use();
			Texture.Bind(TextureUnit.Texture0);
			Shader.SetInt("uTexture", 0);
			Shader.SetMat4("uModel", Transform.ViewMatrix);
            Shader.SetMat4("uView", camera.GetViewMatrix());
            Shader.SetMat4("uProjection", camera.GetProjectionMatrix());
            Model.Draw();
        }

        public void Dispose()
        {
            Model.Dispose();
            Shader.Dispose();
            Texture.Dispose();
        }
    }
}
