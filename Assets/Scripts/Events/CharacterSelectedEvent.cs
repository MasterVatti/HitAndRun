using SimpleEventBus.Events;

public class CharacterSelectedEvent : EventBase
{
    public CharacterSettings Character;

    public CharacterSelectedEvent(CharacterSettings character)
    {
        Character = character;
    }
}