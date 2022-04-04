using System;
using SimpleEventBus.Events;
using UnityEngine;

public class BombCountdownStartEvent: EventBase
{
    public int BombTimer;
    public Action OnBombDetonateEffect;
    public GameObject Bomb;
    
    public BombCountdownStartEvent(GameObject bomb, int bombTimer, Action onBombDetonateEffect)
    {
        BombTimer = bombTimer;
        OnBombDetonateEffect = onBombDetonateEffect;
        Bomb = bomb;
    }
}