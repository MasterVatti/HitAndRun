using System.Linq;
using UnityEngine;
using Pools;
using SimpleEventBus.Disposables;

public class ZombieManager : MonoBehaviour
{
    [SerializeField]
    private ZombieSpawnPoint[] _zombieSpawnPoints;

    private CompositeDisposable _subscriptions;
    private int _allZombiesQuantity;

    private void Start()
    {
        foreach (var zombieSpawnPoint in _zombieSpawnPoints)
        {
            _allZombiesQuantity += zombieSpawnPoint.GetZombiesQuantityInPoint();
            zombieSpawnPoint.SpawnZombie();
        }
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BulletHitEvent>(ZombieTakeDamage)
        };
    }
    
    private void ZombieTakeDamage(BulletHitEvent eventData)
    {
        if (eventData.HitObject.tag == "Enemy")
        {
            var zombie = eventData.HitObject.GetComponent<Zombie>();
            var zombieSpawnPoint = eventData.HitObject.GetComponentInParent<ZombieSpawnPoint>();
            zombie.Health -= 1;
            if (zombie.Health == 0)
            {
                zombieSpawnPoint.KillZombie(zombie);
                EventStreams.Game.Publish(new ZombieDeathEvent());
            }
        }
    }

    public int GetAllZombiesQuantity()
    {
        return _allZombiesQuantity;
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}