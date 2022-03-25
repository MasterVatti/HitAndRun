using SimpleEventBus.Events;
using UnityEngine;

public class BulletHitEvent: EventBase
{
    public GameObject HitObject;
    public Bullet Bullet;
    
    public BulletHitEvent(Bullet bullet, GameObject hitObject)
    {
        HitObject = hitObject;
        Bullet = bullet;
    }
}