using SimpleEventBus.Events;

public class CharacterTakeDamageEvent : EventBase
{
    public float ZombieDamage;
    
    public CharacterTakeDamageEvent(float damage)
    {
        ZombieDamage = damage;
    }
}