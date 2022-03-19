using System.Linq;
using UnityEngine;
using Pools;
using SimpleEventBus.Disposables;

public class ZombieManager : MonoBehaviour
{
    [SerializeField] 
    private Zombie _zombiePrefab;
    [SerializeField]
    private Transform[] _zombieSpawnPoint;
    [SerializeField]
    private int[] _zombieCountInOnePoint;
    [SerializeField]
    private int _poolSize = 45;
    [SerializeField]
    private Transform _zombieStorage;

    private CompositeDisposable _subscriptions;
    private MonoBehaviourPool<Zombie> _zombiePool;
    private int _startPoolSize = 0;
    private Transform _currentZombieSpawnPoint;
    private string[] _zombieMaterialName;
    
    private void Awake()
    {
        SetZombieMaterialName();
        _zombiePool = new MonoBehaviourPool<Zombie>(_zombiePrefab, _zombieStorage, _startPoolSize);
        
        for (int i = 0; i < _zombieSpawnPoint.Length; i++)
        {
            SpawnZombie(_zombieSpawnPoint[i], _zombieCountInOnePoint[i], i);
        }
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BulletHitEvent>(ZombieTakeDamage)
        };
    }

    private void SetZombieMaterialName()
    {
        _zombieMaterialName = new string[_zombieSpawnPoint.Length];
        _zombieMaterialName[0] = "SA_Zombie_Businessman";
        _zombieMaterialName[1] = "SA_Zombie_Mechanic";
        _zombieMaterialName[2] = "SA_Zombie_Roadworker";
    }

    private void SpawnZombie(Transform zombieSpawnPoint, int zombieCountInOnePoint, int pointNumber)
    {
        for (int i = 0; i < zombieCountInOnePoint; i++)
        {
            var zombie = _zombiePool.Take();
            SetZombiePosition(zombie, zombieSpawnPoint);
            if (pointNumber > 0)
            {
                zombie.transform.Find(_zombieMaterialName[pointNumber-1]).gameObject.SetActive(false);
                zombie.transform.Find(_zombieMaterialName[pointNumber]).gameObject.SetActive(true);
            }
        }
    }

    private void SetZombiePosition(Zombie zombie, Transform zombieSpawnPoint)
    {
        Vector3 transformPosition = new Vector3();
        transformPosition.z = Random.Range(zombieSpawnPoint.position.z - 8f, zombieSpawnPoint.position.z + 8f);
        transformPosition.x = Random.Range(zombieSpawnPoint.position.x - 8f, zombieSpawnPoint.position.x + 8f);
        transformPosition.y = zombieSpawnPoint.position.y;
        zombie.transform.position = transformPosition;
    }
    
    private void ZombieTakeDamage(BulletHitEvent eventData)
    {
        if (eventData.GameObject.tag == "Enemy")
        {
            var zombie = eventData.GameObject.GetComponent<Zombie>();
            zombie.Health -= 1;
            if (zombie.Health == 0)
            {
                _zombiePool.Release(zombie);
                EventStreams.Game.Publish(new ZombieDeathEvent());
            }
        }
    }

    public int GetZombiesCount()
    {
        return _poolSize;
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}