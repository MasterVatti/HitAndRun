using SimpleEventBus.Events;
using UnityEngine;

public class ZombieTakeDamageEvent : EventBase
{
    public float DamageAmount;
    public GameObject ZombiePrefab;
    
    public ZombieTakeDamageEvent(GameObject zombiePrefab, float damageAmount)
    {
        DamageAmount = damageAmount;
        ZombiePrefab = zombiePrefab;
    }
}