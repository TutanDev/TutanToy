using Silk.NET.OpenGL;
using ToyEngine.Render;
using Shader = ToyEngine.Render.Shader;
using Texture = ToyEngine.Render.Texture;

namespace ToyEngine.Base
{
    public class ToyObject : IDisposable
    {
        public Transform[] Transforms { get; set; }
        public Model Model { get; set; }
        public Shader Shader { get; set; }
        public Texture Texture { get; set; }


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
