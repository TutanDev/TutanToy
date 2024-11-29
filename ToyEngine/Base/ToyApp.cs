using Silk.NET.Windowing;
using ToyEngine.Platform.Interfaces;
using ToyEngine.Render;

namespace ToyEngine.Base;

public class ToyApp
{
	IWindowingPlatform _windowing;
	IWindow _window;

	protected IRenderer _renderer;

	public ToyApp() { }


    public virtual void RegisterServices()
    {
        // Input here
    }

    public virtual void Initialize()
    {
        _windowing = ToyLocator.Current.GetRequiredService<IWindowingPlatform>();
        _renderer = ToyLocator.Current.GetRequiredService<IRenderer>();

        _window = _windowing.CreateWindow();
        _window.Load += OnWindowLoad;
        _window.Update += OnUpdate;
        _window.Render += Render;
        _window.Closing += OnWindowClose;

        _window.Run();
        _window.Dispose();
    }

    public virtual void OnUpdate(double deltaTime) { }

    public virtual void OnRender(double deltaTime) { }
    public virtual void OnUIRender(double deltaTime) { }

	private void OnWindowLoad()
	{
		_renderer.Init(_window, _windowing.CreateInput());
	}

	private void Render(double deltaTime)
	{
		_renderer.BeginScene();
		OnRender(deltaTime);
		_renderer.EndScene();

		_renderer.BeginUI((float)deltaTime);
		OnUIRender(deltaTime);
		_renderer.EndUI();
	}

	private void OnWindowClose()
	{
		_renderer.Destroy();
	}
}
