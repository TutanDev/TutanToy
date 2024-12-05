
using Silk.NET.OpenGL;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Render;

public class Mesh : IDisposable
{
	private GL _gl;

	private IVertexBuffer _vbo;
	private IIndexBuffer _ebo;
	private IVertexArray _vao;

	public IReadOnlyList<ITexture> Textures { get; private set; }

	readonly float[] _vertices;
	readonly uint[] _indices;

	private uint stride = 5;
	private uint vertcount = 36;

	public Mesh(in float[] vertices, in uint[] indices, in List<ITexture> textures)
	{
		_vertices = vertices;
		_indices = indices;
		Textures = textures;
	}

	public void SetRenderContext(in GL gl)
	{
		_gl = gl;

		CreateMesh();
	}

	public unsafe void Draw()
	{
		_vao.Bind();
		//_gl.DrawElements(PrimitiveType.Triangles, vertcount, DrawElementsType.UnsignedInt, null);
		_gl.DrawArrays(PrimitiveType.Triangles, 0, vertcount);
	}

	public void Dispose()
	{
		Textures = null;

		_vbo.Dispose();
		_ebo.Dispose();
		_vao.Dispose();
	}

	private void CreateMesh()
	{
		_vbo = VertexBuffer.Create(_vertices);
		_ebo = IndexBuffer.Create(_indices);

		_vao = Renderer.Interfaces.VertexArray.Create();
		_vao.AddvertexBuffer(_vbo);
		_vao.SetIndexBuffer(_ebo);
		_vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, stride, 0);
		_vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, stride, 3);

		_gl.BindVertexArray(0);
		_gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
		_gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
	}
}
