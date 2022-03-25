using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private void Start()
    {
        EventStreams.Game.Publish(new CharacterInstantiatedEvent(gameObject));
    }
}