using SimpleEventBus.Events;

public class CharacterStateEvent : EventBase
{
    public bool IsCharacterWin;
    
    public CharacterStateEvent(bool state)
    {
        IsCharacterWin = state;
    }
}