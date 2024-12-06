namespace ToyEngine.Renderer.Interfaces;

public interface IBuffer<T> : IDisposable
{
    void Bind();
    void Unbind();
}

public interface IVertexBuffer : IBuffer<float>
{
    void SetData(Span<float> data);
    //void SetLayout(BufferLayout layout);
    //BufferLayout GetLayout();

}

public interface IIndexBuffer : IBuffer<int>
{
    //public int GetCount();
}
