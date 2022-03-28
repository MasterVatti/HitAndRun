using UnityEngine;
using SimpleEventBus.Disposables;

public class CharacterController : MonoBehaviour
{
    [SerializeField] 
    private Transform _characterSpawnPoint;
    [SerializeField] 
    private GameObject _characterTakeDamageFX;
    
    private CompositeDisposable _subscriptions;
    private GameObject _currentCharacter;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterTakeDamageEvent>(SpawnTakeDamageFX)
        };
    }
    
    private void SpawnTakeDamageFX(CharacterTakeDamageEvent eventData)
    {
        var takeDamageFX = Instantiate(_characterTakeDamageFX, transform);
        takeDamageFX.transform.position = _currentCharacter.transform.position;
        takeDamageFX.transform.rotation = Quaternion.Euler(0, 0, 90f);
    }
    
    public void Initialize(CharacterSettings character)
    {
        var newCharacter = Instantiate(character.Prefab);
        newCharacter.transform.position = _characterSpawnPoint.position;
        _currentCharacter = newCharacter;
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}