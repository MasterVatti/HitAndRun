using SimpleEventBus.Events;
using UnityEngine;

public class BombCountdownEndEvent: EventBase
{
    public GameObject Bomb;
    
    public BombCountdownEndEvent(GameObject bomb)
    {
        Bomb = bomb;
    }
}