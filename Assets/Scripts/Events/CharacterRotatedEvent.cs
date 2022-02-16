using SimpleEventBus.Events;

public class CharacterRotatedEvent : EventBase
{
    public float Delta;

    public CharacterRotatedEvent(float delta)
    {
        Delta = delta;
    }
}