using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int Health = 2;

    private void ZombieAttack()
    {
        EventStreams.Game.Publish(new CharacterTakeDamageEvent());
    }
}