
namespace ToyEngine.Base;

public record struct RuntimePlatformInfo
{
	public FormFactorType FormFactor => IsDesktop ? FormFactorType.Desktop :
		IsMobile ? FormFactorType.Mobile : IsTV ? FormFactorType.TV : FormFactorType.Unknown;
	public bool IsDesktop { get; set; }
	public bool IsMobile { get; set; }
	public bool IsTV { get; set; }
}

public enum FormFactorType
{
	Unknown,
	Desktop,
	Mobile,
	TV
}

public class RuntimePlatform 
{
    public static RuntimePlatformInfo GetRuntimeInfo() => new()
    {
        IsDesktop = OperatingSystem.IsWindows()
                    || OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst()
                    || OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD(),
        IsMobile = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS() && !OperatingSystem.IsMacCatalyst(),
        IsTV = OperatingSystem.IsTvOS()
    };

    public static RendererAPI GetRendererAPI()
    {
        return RendererAPI.OpenGL;
    }
}

public enum RendererAPI
{
	None,
	OpenGL,
}