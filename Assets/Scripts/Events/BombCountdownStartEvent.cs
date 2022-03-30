using System;
using SimpleEventBus.Events;

public class BombCountdownStartEvent: EventBase
{
    public int BombTimer;
    public Action OnBombDetonateEffect;
    
    public BombCountdownStartEvent(int bombTimer, Action onBombDetonateEffect)
    {
        BombTimer = bombTimer;
        OnBombDetonateEffect = onBombDetonateEffect;
    }
}