using SimpleEventBus.Events;

public class CharacterSelectedEvent : EventBase
{
    public CharacterSettings CharacterSettings;

    public CharacterSelectedEvent(CharacterSettings characterSettings)
    {
        CharacterSettings = characterSettings;
    }
}