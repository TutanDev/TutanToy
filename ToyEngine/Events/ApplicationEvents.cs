
namespace ToyEngine.Events;

public sealed class WindowResizeEvent : Event
{
	public static readonly EventType StaticType = EventType.WindowResize;

	public readonly int Width;
	public readonly int Height;

	public WindowResizeEvent(int width, int height)
	{
		Width = width;
		Height = height;
	}

	public override EventType GetEventType() => StaticType;
	public override EventCategory GetCategoryFlags() => EventCategory.Application;

	public override string ToString() => $"{nameof(WindowResizeEvent)}: {Width}x{Height}";
}


public sealed class WindowCloseEvent : Event
{
	public static readonly EventType StaticType = EventType.WindowClose;

	public override EventType GetEventType() => StaticType;
	public override EventCategory GetCategoryFlags() => EventCategory.Application;

	public override string ToString() => $"{nameof(WindowResizeEvent)}";
}

public sealed class AppUpdateEvent : Event
{
	public static readonly EventType StaticType = EventType.AppUpdate;

	public override EventType GetEventType() => StaticType;
	public override EventCategory GetCategoryFlags() => EventCategory.Application;

	public override string ToString() => $"{nameof(AppUpdateEvent)}";
}


public sealed class AppRenderEvent : Event
{
	public static readonly EventType StaticType = EventType.AppRender;

	public override EventType GetEventType() => StaticType;
	public override EventCategory GetCategoryFlags() => EventCategory.Application;

	public override string ToString() => $"{nameof(AppRenderEvent)}";
}