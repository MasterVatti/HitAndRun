using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private ZombieRightHand _zombieRightHand;
    [SerializeField]
    public float Health = 2;

    private void ZombieAttack()
    {
        if (_zombieRightHand.GetCollisionState())
        {
            EventStreams.Game.Publish(new CharacterTakeDamageEvent());
        }
    }
}