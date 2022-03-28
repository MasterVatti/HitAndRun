using System;
using UnityEngine;
using SimpleEventBus.Disposables;
using Random = UnityEngine.Random;
using System.Collections;

public class Character : MonoBehaviour
{
    [SerializeField] 
    private Animator _animator;

    private CompositeDisposable _subscriptions;

    private static readonly int killing = Animator.StringToHash(GlobalConstants.CHARACTER_ANIMATOR_SELECT_DEATH_ANIMATION);
    public int KillCharacterAnimation
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
        KillCharacterAnimation = Random.Range(0, 2);
        _animator.SetTrigger(GlobalConstants.CHARACTER_ANIMATOR_DEATH_TRIGGER);
        StartCoroutine(GoGameOverState());
    }

    private IEnumerator GoGameOverState()
    {
        _subscriptions?.Dispose();
        EventStreams.Game.Publish(new GameOverLightChangeEvent());
        yield return new WaitForSeconds(3f);
        EventStreams.Game.Publish(new GameOverEvent());
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}