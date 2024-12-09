using ToyEngine.Platform.API;

namespace ToyEngine.Events;


public sealed class MouseMovedEvent : Event
{
	public static readonly EventType StaticType = EventType.MouseMoved;

	public readonly float X;
	public readonly float Y;

	public MouseMovedEvent(float x, float y) => (X, Y) = (x, y);

	public override EventType GetEventType() => StaticType;
	public override EventCategory GetCategoryFlags() => EventCategory.Mouse | EventCategory.Input;

	public override string ToString() => $"{nameof(MouseMovedEvent)}: position({X}, {Y})";
}

public sealed class MouseScrolledEvent : Event
{
	public static readonly EventType StaticType = EventType.MouseMoved;

	public readonly float X;
	public readonly float Y;

	public MouseScrolledEvent(float x, float y) => (X, Y) = (x, y);

	public override EventType GetEventType() => StaticType;
	public override EventCategory GetCategoryFlags() => EventCategory.Mouse | EventCategory.Input;

	public override string ToString() => $"{nameof(MouseScrolledEvent)}: offset({X}, {Y})";
}

public abstract class MouseButtonEvent : Event
{
	public readonly MouseCode MouseCode;

	public MouseButtonEvent(MouseCode mouseCode) => MouseCode = mouseCode;

	public override EventCategory GetCategoryFlags() => EventCategory.MouseButton | EventCategory.Mouse | EventCategory.Input;
}

public sealed class MouseButtonPressedEvent : MouseButtonEvent
{
	public static readonly EventType StaticType = EventType.MouseButtonPressed;

	public readonly bool IsRepeat;

	public MouseButtonPressedEvent(MouseCode mouseCode) 
		: base(mouseCode) { }

	public override EventType GetEventType() => StaticType;

	public override string ToString() => $"{nameof(MouseButtonPressedEvent)}: {MouseCode}";
}

public sealed class MouseButtonReleasedEvent : MouseButtonEvent
{
	public static readonly EventType StaticType = EventType.MouseButtonReleased;

	public readonly bool IsRepeat;

	public MouseButtonReleasedEvent(MouseCode mouseCode)
		: base(mouseCode) { }

	public override EventType GetEventType() => StaticType;

	public override string ToString() => $"{nameof(MouseButtonPressedEvent)}: {MouseCode}";
}