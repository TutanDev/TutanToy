using Silk.NET.OpenGL;
using System.Numerics;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Renderer.OpenGL;

internal class OpenGLShader : IShader
{
    private readonly GL _gl;
    private uint _handle;

    private readonly string _vertexPath;
    private readonly string _fragmentPath;

    public OpenGLShader(in string vertexPath, in string fragmentPath)
    {
        _gl = OpenGLContext.GL;
        _vertexPath = vertexPath;
        _fragmentPath = fragmentPath;

        Compile();
    }


    public void Compile()
    {
        var vertexCode = File.ReadAllText(_vertexPath);
        var fragmentCode = File.ReadAllText(_fragmentPath);

        uint vertexID = CompileShader(ShaderType.VertexShader, vertexCode);
        uint fragmentID = CompileShader(ShaderType.FragmentShader, fragmentCode);

        _handle = _gl.CreateProgram();
        _gl.AttachShader(_handle, vertexID);
        _gl.AttachShader(_handle, fragmentID);
        _gl.LinkProgram(_handle);

        _gl.GetProgram(_handle, ProgramPropertyARB.LinkStatus, out int lStatus);
        if (lStatus != (int)GLEnum.True)
            throw new Exception($"Program failed to link, error: {_gl.GetProgramInfoLog(_handle)}");

        _gl.DetachShader(_handle, vertexID);
        _gl.DetachShader(_handle, fragmentID);
        _gl.DeleteShader(vertexID);
        _gl.DeleteShader(fragmentID);
    }

    public void Use() => _gl.UseProgram(_handle);



    public void SetInt(string name, int value)
    {
        var location = _gl.GetUniformLocation(_handle, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }
        _gl.Uniform1(location, value);
    }

    public unsafe void SetMat4(string name, Matrix4x4 value)
    {
        int location = _gl.GetUniformLocation(_handle, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }
        _gl.UniformMatrix4(location, 1, false, (float*)&value);
    }

    public void Dispose() => _gl.DeleteProgram(_handle);


    // Private
    private uint CompileShader(ShaderType type, string shaderCode)
    {
        uint shaderId = _gl.CreateShader(type);
        _gl.ShaderSource(shaderId, shaderCode);
        _gl.CompileShader(shaderId);

        _gl.GetShader(shaderId, ShaderParameterName.CompileStatus, out int vStatus);
        if (vStatus != (int)GLEnum.True)
            throw new Exception($"Error compiling shader of type {type}, failed with error {_gl.GetShaderInfoLog(shaderId)}");

        return shaderId;
    }
}
