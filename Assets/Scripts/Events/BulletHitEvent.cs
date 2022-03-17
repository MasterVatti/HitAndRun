using SimpleEventBus.Events;
using UnityEngine;

public class BulletHitEvent: EventBase
{
    public GameObject GameObject;
    public Bullet Bullet;
    
    public BulletHitEvent(Bullet bullet, GameObject gameObject)
    {
        GameObject = gameObject;
        Bullet = bullet;
    }
}