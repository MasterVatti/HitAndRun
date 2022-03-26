using System;
using UnityEngine;
using SimpleEventBus.Disposables;
using Random = UnityEngine.Random;


public class Character : MonoBehaviour
{
    [SerializeField] 
    private Animator _animator;
    
    private CompositeDisposable _subscriptions;
    
    private static readonly int killing = Animator.StringToHash(GlobalConstants.CHARACTER_ANIMATOR_SELECT_DEATH_ANIMATION);
    public int KillCharacter
    {
        get => _animator.GetInteger(killing);
        set => _animator.SetInteger(killing, value);
    }
    
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterDeathEvent>(CharacterKill)
        };
    }
    
    private void Start()
    {
        EventStreams.Game.Publish(new CharacterInstantiatedEvent(gameObject));
    }
    
    private void CharacterKill(CharacterDeathEvent eventData)
    {
        _animator.SetTrigger(GlobalConstants.CHARACTER_ANIMATOR_DEATH_TRIGGER);
        KillCharacter = Random.Range(0, 2);
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}