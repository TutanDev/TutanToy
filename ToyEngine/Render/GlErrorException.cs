using Silk.NET.OpenGL;

namespace ToyEngine.Render;

public class GlErrorException : Exception
{
	public GlErrorException(string message) : base(message) { }

	public static void ThrowIfError(GL Gl)
	{
		GLEnum error = Gl.GetError();
		if (error != GLEnum.NoError)
		{
			throw new GlErrorException(error.ToString());
		}
	}
}
