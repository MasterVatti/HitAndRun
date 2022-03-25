using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private ZombieDeathCounter _deathCounter;

    public void Initialize(float zombiesCount)
    {
        _deathCounter.Initialize(zombiesCount);
    }
}