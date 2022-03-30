using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    [SerializeField]
    private ZombieSpawnPoint[] _zombieSpawnPoints;
    
    private int _allZombiesQuantity;

    private void Start()
    {
        foreach (var zombieSpawnPoint in _zombieSpawnPoints)
        {
            _allZombiesQuantity += zombieSpawnPoint.GetZombiesQuantityInPoint();
            zombieSpawnPoint.SpawnZombie();
        }
    }

    public int GetAllZombiesQuantity()
    {
        return _allZombiesQuantity;
    }
}