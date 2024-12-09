
using ToyEngine.Platform.API;

namespace ToyEngine.Events;


public abstract class KeyEvent : Event
{
	public readonly KeyCode KeyCode;

	public KeyEvent(KeyCode keyCode) => KeyCode = keyCode;

	public override EventCategory GetCategoryFlags() => EventCategory.Keyboard | EventCategory.Input;
}

public sealed class KeyPressedEvent : KeyEvent
{
	public static readonly EventType StaticType = EventType.KeyPressed;

	public readonly bool IsRepeat;

	public KeyPressedEvent(KeyCode keyCode, bool isRepeat = false)
		: base(keyCode) => IsRepeat = isRepeat;


	public override EventType GetEventType() => StaticType;

	public override string ToString() => $"{nameof(KeyPressedEvent)}: {KeyCode} (repeat = {IsRepeat})";
}

public sealed class KeyReleasedEvent : KeyEvent
{
	public static readonly EventType StaticType = EventType.KeyReleased;


	public KeyReleasedEvent(KeyCode keyCode) : base(keyCode) { }


	public override EventType GetEventType() => StaticType;

	public override string ToString() => $"{nameof(KeyReleasedEvent)}: {KeyCode}";
}

public sealed class KeyTypedEvent : KeyEvent
{
	public static readonly EventType StaticType = EventType.KeyTyped;


	public KeyTypedEvent(KeyCode keyCode) : base(keyCode) { }


	public override EventType GetEventType() => StaticType;

	public override string ToString() => $"{nameof(KeyTypedEvent)}: {KeyCode}";
}
