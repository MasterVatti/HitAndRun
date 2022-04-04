using System;
using SimpleEventBus.Disposables;
using UnityEngine;

public class FirstAidKitManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _firstAidKit;
    [SerializeField] 
    private int _count;
    [SerializeField] 
    private float[] _oneFirstAidKitPositionX;
    [SerializeField] 
    private float[] _oneFirstAidKitPositionZ;
    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<FirstAidKitActivatedEvent>(DisableFirstAidKit)
        };
    }

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            var bomb = Instantiate(_firstAidKit, transform);
            bomb.transform.position = new Vector3(_oneFirstAidKitPositionX[i], 0f, _oneFirstAidKitPositionZ[i]);
        }
    }
    
    private void DisableFirstAidKit(FirstAidKitActivatedEvent eventData)
    {
        Destroy(eventData.FirstAidKit);
        eventData.FirstAidKit.GetComponent<FirstAidKit>().DestroyEffects();
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}