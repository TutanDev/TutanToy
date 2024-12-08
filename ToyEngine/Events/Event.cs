
using System.Runtime.CompilerServices;

namespace ToyEngine.Events;


public enum EventType
{
    None = 0,
    WindowClose,
    WindowResize,
    WindowFocus,
    WindowLostFocus,
    WindowMoved,
    AppTick,
    AppUpdate,
    AppRender,
    KeyPressed,
    KeyReleased,
    KeyTyped,
    MouseButtonPressed,
    MouseButtonReleased,
    MouseMoved,
    MouseScrolled
}

[Flags]
public enum EventCategory
{
    None = 0,
    Application = 1 << 0,
    Input = 1 << 1,
    Keyboard = 1 << 2,
    Mouse = 1 << 3,
    MouseButton = 1 << 4
}

public interface IEvent
{
    EventType GetEventType();
    EventCategory GetCategoryFlags();
    bool Handled { get; set; }
}

public abstract class Event : IEvent
{
    public bool Handled { get; set; }

    public abstract EventType GetEventType();
    public abstract EventCategory GetCategoryFlags();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsInCategory(EventCategory category)
    {
        return (GetCategoryFlags() & category) != 0;
    }
}

public sealed class EventDispatcher
{
    private readonly IEvent _event;

    public EventDispatcher(IEvent eventInstance)
    {
        _event = eventInstance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Dispatch<T>(Func<T, bool> func) where T : IEvent
    {
        if (_event is T specificEvent)
        {
            _event.Handled |= func(specificEvent);
            return true;
        }
        return false;
    }
}
