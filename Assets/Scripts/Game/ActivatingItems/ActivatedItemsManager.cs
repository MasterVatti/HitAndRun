using SimpleEventBus.Disposables;
using UnityEngine;
using System.Collections;

public class ActivatedItemsManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _firstAidKit;
    [SerializeField] 
    private GameObject[] _bombEffects;

    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<FirstAidKitActivatedEvent>(DisableFirstAidKit),
            EventStreams.Game.Subscribe<BombCountdownStartEvent>(DisableBombFX)
        };
    }

    private void DisableFirstAidKit(FirstAidKitActivatedEvent eventData)
    {
        Destroy(_firstAidKit);
    }
    
    private void DisableBombFX(BombCountdownStartEvent eventData)
    {
        foreach (var bombEffect in _bombEffects)
        {
            Destroy(bombEffect);
        }
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}