
using Silk.NET.OpenGL;

namespace ToyEngine.Render;

public class Mesh : IDisposable
{
	private GL _gl;

	private BufferObject<float> _vbo;
	private BufferObject<uint> _ebo;
	private VertexArrayObject<float, uint> _vao;

	public IReadOnlyList<Texture> Textures { get; private set; }

	readonly float[] _vertices;
	readonly uint[] _indices;

	private uint stride = 5;
	private uint vertcount = 36;

	public Mesh(in float[] vertices, in uint[] indices, in List<Texture> textures)
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
		_vbo = new(_gl, _vertices, BufferTargetARB.ArrayBuffer);
		_ebo = new(_gl, _indices, BufferTargetARB.ElementArrayBuffer);
		_vao = new(_gl, _vbo, _ebo);

		_vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, stride, 0);
		_vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, stride, 3);

		_gl.BindVertexArray(0);
		_gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
		_gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
	}
}
