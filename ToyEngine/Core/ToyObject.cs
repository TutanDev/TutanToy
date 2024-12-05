using Silk.NET.OpenGL;
using ToyEngine.Render;
using ToyEngine.Renderer.Interfaces;


namespace ToyEngine.Core
{
    public class ToyObject : IDisposable
    {
        public Transform[] Transforms { get; set; }
        public Model Model { get; set; }
        public IShader Shader { get; set; }
        public ITexture Texture { get; set; }


        public void SetRenderContext(GL gl)
        {
            Model.SetRenderContext(gl);
            Texture.SetRenderContext(gl);
        }

        public void Dispose()
        {
            Model.Dispose();
            Shader.Dispose();
            Texture.Dispose();
        }
    }
}
