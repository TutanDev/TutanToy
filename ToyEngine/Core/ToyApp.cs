using ToyEngine.Platform.Interfaces;
using ToyEngine.Renderer.Interfaces;

namespace ToyEngine.Core;

public class ToyApp
{
	IWindowingPlatform _windowing;
	protected IRenderer _renderer;

	private bool _running = false;

	public ToyApp() { }


    public virtual void RegisterServices()
    {
        // Input here
    }

    public virtual void Initialize()
    {
        _windowing = ToyLocator.Current.GetRequiredService<IWindowingPlatform>();
        _renderer = ToyLocator.Current.GetRequiredService<IRenderer>();

		_renderer.Initialize(_windowing.GetGraphicsContext());
    }

    public virtual void OnUpdate(double deltaTime) { }

    public virtual void OnRender(double deltaTime) { }
    public virtual void OnUIRender(double deltaTime) { }

	public void Run()
	{
		_running = true;
		var deltaTime = 0.2f;

		while (_running)
		{
			OnUpdate(deltaTime);
			OnRender(deltaTime);

			//_renderer.BeginUI(deltaTime);
			//OnUIRender(deltaTime);
			//_renderer.EndUI();

			_windowing.OnUpdate();
		}
	}

	private void OnWindowClose()
	{
		_running = false;
		_renderer.Dispose();
	}
}
