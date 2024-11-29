
namespace ToyEngine.Base;

public class ToyLocator : IDependencyResolver
{
	public static ToyLocator Current { get; set; }

	private static readonly IDependencyResolver _current;
	private readonly Dictionary<Type, Func<object?>> _registry = new Dictionary<Type, Func<object?>>();

	static ToyLocator()
	{
		_current = Current = new ToyLocator();
	}

	public ToyLocator()
	{

	}

	public object? GetService(Type t)
	{
		return _registry.TryGetValue(t, out var rv) ? rv() : null;
	}

	public class RegistrationHelper<TService>
	{
		private readonly ToyLocator _locator;

		public RegistrationHelper(ToyLocator locator)
		{
			_locator = locator;
		}

		public ToyLocator ToConstant<TImpl>(TImpl constant) where TImpl : TService
		{
			_locator._registry[typeof(TService)] = () => constant;
			return _locator;
		}

		public ToyLocator ToFunc<TImlp>(Func<TImlp> func) where TImlp : TService
		{
			_locator._registry[typeof(TService)] = () => func();
			return _locator;
		}

		public ToyLocator ToLazy<TImlp>(Func<TImlp> func) where TImlp : TService
		{
			var constructed = false;
			TImlp? instance = default;
			_locator._registry[typeof(TService)] = () =>
			{
				if (!constructed)
				{
					instance = func();
					constructed = true;
				}

				return instance;
			};
			return _locator;
		}

		public ToyLocator ToSingleton<TImpl>() where TImpl : class, TService, new()
		{
			TImpl? instance = null;
			return ToFunc(() => instance ?? (instance = new TImpl()));
		}

		public ToyLocator ToTransient<TImpl>() where TImpl : class, TService, new() => ToFunc(() => new TImpl());
	}

	public RegistrationHelper<T> Bind<T>() => new RegistrationHelper<T>(this);


	public ToyLocator BindToSelf<T>(T constant)
		=> Bind<T>().ToConstant(constant);

	public ToyLocator BindToSelfSingleton<T>() where T : class, new() => Bind<T>().ToSingleton<T>();
}

public interface IDependencyResolver
{
	object? GetService(Type t);
}

public static class LocatorExtensions
{
	public static T? GetService<T>(this IDependencyResolver resolver)
	{
		return (T?)resolver.GetService(typeof(T));
	}

	public static object GetRequiredService(this IDependencyResolver resolver, Type t)
	{
		return resolver.GetService(t) ?? throw new InvalidOperationException($"Unable to locate '{t}'.");
	}

	public static T GetRequiredService<T>(this IDependencyResolver resolver)
	{
		return (T?)resolver.GetService(typeof(T)) ?? throw new InvalidOperationException($"Unable to locate '{typeof(T)}'.");
	}
}
