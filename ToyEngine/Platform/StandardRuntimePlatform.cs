using ToyEngine.Platform.Interfaces;

namespace ToyEngine.Platform;

public class StandardRuntimePlatform : IRuntimePlatform
{
	public virtual RuntimePlatformInfo GetRuntimeInfo() => new()
	{
		IsDesktop = OperatingSystemExtensions.IsWindows()
					|| OperatingSystemExtensions.IsMacOS() || OperatingSystemExtensions.IsMacCatalyst()
					|| OperatingSystemExtensions.IsLinux() || OperatingSystemExtensions.IsFreeBSD(),
		IsMobile = OperatingSystemExtensions.IsAndroid() || (OperatingSystemExtensions.IsIOS() && !OperatingSystemExtensions.IsMacCatalyst()),
		IsTV = OperatingSystemExtensions.IsTvOS()
	};
}


