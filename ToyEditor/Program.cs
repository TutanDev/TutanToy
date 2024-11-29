
using ToyEditor.Importers;
using ToyEngine;
using ToyEngine.Base;
using ToyEngine.Render;

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
