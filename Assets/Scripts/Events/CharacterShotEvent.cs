using SimpleEventBus.Events;
using UnityEngine;

public class CharacterShotEvent : EventBase
{
    public Quaternion CharacterTransformRotation;
    
    public CharacterShotEvent(Quaternion characterTransformRotation)
    {
        CharacterTransformRotation = characterTransformRotation;
    }
}