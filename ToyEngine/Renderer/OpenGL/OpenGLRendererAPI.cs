using Silk.NET.OpenGL;
using System.Drawing;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Renderer.OpenGL;

internal class OpenGLRendererAPI : IRendererAPI
{
    GL _gl;

    public OpenGLRendererAPI(GL gl)
    {
        _gl = gl;
    }

    public void Initialize()
    {
        _gl.Enable(EnableCap.DepthTest);
    }

    public void SetViewPort(int x, int y, int width, int height)
        => _gl.Viewport(x, y, (uint)width, (uint)height);

    public void SetClearColor(Color color) => _gl.ClearColor(color);
    public void Clear() => _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


    public void DrawArrays(IVertexArray vertexArray)
    {
        vertexArray.Bind();
        _gl.DrawArrays(PrimitiveType.Triangles, 0, 36);
    }

    public unsafe void DrawIndexed(IVertexArray vertexArray, int indexcount)
    {
        vertexArray.Bind();
        _gl.DrawElements(PrimitiveType.Triangles, (uint)indexcount, DrawElementsType.UnsignedInt, null);
    }
}
