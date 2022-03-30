using UnityEngine;
using Random = UnityEngine.Random;
using Pools;

public class ZombieSpawnPoint : MonoBehaviour
{
    [SerializeField]
    private int _zombieCount;
    [SerializeField] 
    private Zombie _zombiePrefab;
    [SerializeField] 
    public GameObject ZombieDamageFX;

    private MonoBehaviourPool<Zombie> _zombiePool;
    private Transform _zombieSpawnPoint;
    
    private void Awake()
    {
        _zombieSpawnPoint = transform;
        _zombiePool = new MonoBehaviourPool<Zombie>(_zombiePrefab, _zombieSpawnPoint, _zombieCount);
    }

    public void SpawnZombie()
    {
        for (int i = 0; i < _zombieCount; i++)
        {
            var zombie = _zombiePool.Take();
            SetZombiePosition(zombie);
        }
    }

    public void KillZombie(Zombie zombie)
    {
        _zombiePool.Release(zombie);
    }
    
    private void SetZombiePosition(Zombie zombie)
    {
        Vector3 transformPosition = new Vector3();
        transformPosition.z = Random.Range(_zombieSpawnPoint.position.z - 7f, _zombieSpawnPoint.position.z + 7f);
        transformPosition.x = Random.Range(_zombieSpawnPoint.position.x - 7f, _zombieSpawnPoint.position.x + 7f);
        transformPosition.y = _zombieSpawnPoint.position.y;
        zombie.transform.position = transformPosition;
    }
    
    public int GetZombiesQuantityInPoint()
    {
        return _zombieCount;
    }
}