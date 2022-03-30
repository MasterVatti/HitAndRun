using SimpleEventBus.Disposables;
using UnityEngine;

public class ZombieTakeDamage : MonoBehaviour
{
    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<ZombieTakeDamageEvent>(ZombieTakeDamageHandler)
        };
    }

    private void ZombieTakeDamageHandler(ZombieTakeDamageEvent eventData)
    {
        var zombie = eventData.ZombiePrefab.GetComponent<Zombie>();
        var zombieSpawnPoint = eventData.ZombiePrefab.GetComponentInParent<ZombieSpawnPoint>();
        zombie.Health -= eventData.DamageAmount;
        SpawnZombieTakeDamageFX(zombieSpawnPoint, zombie);
        if (zombie.Health <= 0)
        {
            zombieSpawnPoint.KillZombie(zombie);
            EventStreams.Game.Publish(new ZombieDeathEvent());
        }
    }

    private void SpawnZombieTakeDamageFX(ZombieSpawnPoint zombieSpawnPoint, Zombie zombie)
    {
        var zombieDamageFX = Instantiate(zombieSpawnPoint.ZombieDamageFX, zombieSpawnPoint.transform);
        zombieDamageFX.transform.position = zombie.transform.position;
        zombieDamageFX.transform.rotation = Quaternion.Euler(0, 90f, 0);
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}
