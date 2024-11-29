using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using ToyEngine.Platform;
using ToyEngine.Platform.Interfaces;
using ToyEngine.Render;

namespace ToyEngine.Base;

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

    public ToyAppBuilder UsePlatformDetect()
    {
        if (OperatingSystemExtensions.IsWindows())
        {
            //AssetLoader.RegisterResUriParsers();
            ToyLocator.Current
                .Bind<IRuntimePlatform>().ToSingleton<StandardRuntimePlatform>()
                .Bind<IAssetLoader>().ToConstant(new StandardAssetLoader())
                .Bind<IWindowingPlatform>().ToConstant(new Win32Platform())
                .Bind<IRenderer>().ToConstant(new OpenGLRenderer());
        }
        else
        {
            throw new Exception("Unsupported Platform");
        }

        return Self;
    }


    /// <summary>
    /// Specifies a windowing subsystem to use.
    /// </summary>
    /// <param name="initializer">The method to call to initialize the windowing subsystem.</param>
    /// <returns>An <see cref="ToyAppBuilder"/> instance.</returns>
    public ToyAppBuilder UseWindowingSubsystem(Action initializer)
    {
        WindowingSubsystemInitializer = initializer;
        return Self;
    }

    /// Specifies a rendering subsystem to use.
    /// </summary>
    /// <param name="initializer">The method to call to initialize the rendering subsystem.</param>
    /// <returns>An <see cref="AppBuilder"/> instance.</returns>
    public ToyAppBuilder UseRenderingSubsystem(Action initializer)
    {
        RenderingSubsystemInitializer = initializer;
        return Self;
    }


    /// <summary>
    /// Sets up the platform-specific services for the <see cref="Application"/>.
    /// </summary>
    private void Setup()
    {
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

        WindowingSubsystemInitializer?.Invoke();
        RenderingSubsystemInitializer?.Invoke();

        Instance = _appFactory();
        Instance.RegisterServices();
        Instance.Initialize();
    }
}
