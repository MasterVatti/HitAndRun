using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private void Awake()
    {
        EventStreams.Game.Publish(new CharacterInstantiatedEvent(gameObject));
    }
}