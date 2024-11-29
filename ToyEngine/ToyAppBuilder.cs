namespace ToyEngine;

public class ToyAppBuilder
{
	private static bool s_setupWasAlreadyCalled;

	public ToyApp Instance { get; private set; }

	public Type? ApplicationType { get; private set; }

	public Action WindowingSubsystemInitializer { get; private set; }

	public Action? RenderingSubsystemInitializer { get; private set; }


	private Func<ToyApp> _appFactory;

	private ToyAppBuilder()
    { 
	}


	public void Start()
	{
		Setup();
	}
	public delegate void AppMainDelegate(ToyApp app, string[] args);
	public void Start(AppMainDelegate main, string[] args)
	{
		Setup();
		main(Instance!, args);
	}

	/// <summary>
	/// Begin configuring an <see cref="Application"/>.
	/// </summary>
	/// <typeparam name="TApp">The subclass of <see cref="ToyApp"/> to configure.</typeparam>
	/// <returns>An <see cref="ToyAppBuilder"/> instance.</returns>
	public static ToyAppBuilder Configure<TApp>() where TApp : ToyApp, new()
	{
		return new ToyAppBuilder()
		{
			ApplicationType = typeof(TApp),
			_appFactory = () => new TApp()
		};
	}

	private ToyAppBuilder Self => this;

	/// <summary>
	/// Specifies a windowing subsystem to use.
	/// </summary>
	/// <param name="initializer">The method to call to initialize the windowing subsystem.</param>
	/// <param name="name">The name of the windowing subsystem.</param>
	/// <returns>An <see cref="ToyAppBuilder"/> instance.</returns>
	public ToyAppBuilder UseWindowingSubsystem(Action initializer)
	{
		WindowingSubsystemInitializer = initializer;
		return Self;
	}

	private ToyObject _toy;
	public ToyAppBuilder UseToy(ToyObject toy)
	{
		_toy = toy;
		return Self;
	}


	/// <summary>
	/// Sets up the platform-specific services for the <see cref="Application"/>.
	/// </summary>
	private void Setup()
	{
		//if (RuntimePlatformServicesInitializer == null)
		//{
		//	throw new InvalidOperationException("No runtime platform services configured.");
		//}

		if (WindowingSubsystemInitializer == null)
		{
			//throw new InvalidOperationException("No windowing system configured.");
		}

		if (RenderingSubsystemInitializer == null)
		{
			//throw new InvalidOperationException("No rendering system configured.");
		}

		if (_appFactory == null)
		{
			throw new InvalidOperationException("No Application factory configured.");
		}

		if (s_setupWasAlreadyCalled)
		{
			throw new InvalidOperationException("Setup was already called on one of AppBuilder instances");
		}

		s_setupWasAlreadyCalled = true;
		SetupUnsafe();
	}

	/// <summary>
	/// Setup method that doesn't check for input initalizers being set.
	/// Nor 
	/// </summary>
	internal void SetupUnsafe()
	{
		//_optionsInitializers?.Invoke();
		//RuntimePlatformServicesInitializer?.Invoke();
		
		//RenderingSubsystemInitializer?.Invoke();
		//WindowingSubsystemInitializer?.Invoke();

		Instance = _appFactory();
		Instance.SetToy(_toy);
		Instance.RegisterServices();
		Instance.Initialize();
		Instance.OnFrameworkInitializationCompleted();
	}
}
