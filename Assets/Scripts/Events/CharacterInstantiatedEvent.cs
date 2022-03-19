using SimpleEventBus.Events;
using UnityEngine;

public class CharacterInstantiatedEvent: EventBase
{
    public GameObject Character;

    public CharacterInstantiatedEvent(GameObject character)
    {
        Character = character;
    }
}