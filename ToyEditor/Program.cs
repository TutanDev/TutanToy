using ToyEngine.Core;

namespace ToyEditor;

public class Program
{

	public static void Main(string[] args)
	{
		Directory.SetCurrentDirectory(Path.Combine(AppContext.BaseDirectory, "ToyAssets"));

		ToyAppBuilder.Configure<ToyEditor>()
			.UsePlatformDetect()
			.Start();
	}
}
