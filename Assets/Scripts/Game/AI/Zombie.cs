using System;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private ZombieRightHand _zombieRightHand;
    [SerializeField]
    public float Health;
    [SerializeField]
    public float _damage;

    private void Start()
    {
        NavMeshAgent agent = GetComponent ("NavMeshAgent") as NavMeshAgent;
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition (transform.position, out closestHit, 100f, NavMesh.AllAreas)) {
            transform.position = closestHit.position;
            agent.enabled = true;
        }
    }

    private void ZombieAttack()
    {
        if (_zombieRightHand.GetCollisionCharacter())
        {
            EventStreams.Game.Publish(new CharacterTakeDamageEvent(_damage));
        }
    }
}