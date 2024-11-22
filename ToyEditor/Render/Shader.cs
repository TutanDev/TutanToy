using Silk.NET.OpenGL;
using System.Numerics;

namespace ToyEditor.Render;

internal class Shader : IDisposable
{
    private uint ID;

    private readonly GL _gl;
    private readonly string _vertexPath;
    private readonly string _fragmentPath;

    public Shader(in GL gl, in string vertexPath, in string fragmentPath)
    {
        _gl = gl;
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

        ID = _gl.CreateProgram();
        _gl.AttachShader(ID, vertexID);
        _gl.AttachShader(ID, fragmentID);
        _gl.LinkProgram(ID);

        _gl.GetProgram(ID, ProgramPropertyARB.LinkStatus, out int lStatus);
        if (lStatus != (int)GLEnum.True)
            throw new Exception($"Program failed to link, error: {_gl.GetProgramInfoLog(ID)}");

        _gl.DetachShader(ID, vertexID);
        _gl.DetachShader(ID, fragmentID);
        _gl.DeleteShader(vertexID);
        _gl.DeleteShader(fragmentID);
    }

    public void Use() => _gl.UseProgram(ID);

    public void SetUniform(string name, int value)
    {
        var location = _gl.GetUniformLocation(ID, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }
        _gl.Uniform1(location, value);
    }

    public unsafe void SetUniform(string name, Matrix4x4 value)
    {
        //A new overload has been created for setting a uniform so we can use the transform in our shader.
        int location = _gl.GetUniformLocation(ID, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }
        _gl.UniformMatrix4(location, 1, false, (float*)&value);
    }

    public void Dispose() => _gl.DeleteProgram(ID);

    // PRIVATE
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