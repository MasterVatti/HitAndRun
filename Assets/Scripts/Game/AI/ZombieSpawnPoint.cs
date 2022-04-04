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
    [SerializeField] 
    public float _spawnRangeX;
    [SerializeField] 
    public float _spawnRangeZ;

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
        transformPosition.z = Random.Range(_zombieSpawnPoint.position.z - _spawnRangeZ, _zombieSpawnPoint.position.z + _spawnRangeZ);
        transformPosition.x = Random.Range(_zombieSpawnPoint.position.x - _spawnRangeX, _zombieSpawnPoint.position.x + _spawnRangeX);
        transformPosition.y = _zombieSpawnPoint.position.y;
        zombie.transform.position = transformPosition;
    }
    
    public int GetZombiesQuantityInPoint()
    {
        return _zombieCount;
    }
}