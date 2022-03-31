using System;
using UnityEngine;
using SimpleEventBus.Disposables;

public class Bomb : MonoBehaviour
{
    [SerializeField] 
    private GameObject _explosionFX;
    [SerializeField] 
    private ItemsRotation _bombRotation;
    [SerializeField]
    private int _bombTimer;
    [SerializeField]
    private float _bombExplosionRadius;
    
    [NonSerialized] 
    public Action OnBombDetonateEffect;
    
    private CompositeDisposable _subscriptions;
    private Transform _parentTransform;
    private bool _isBombActivated;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BombCountdownEndEvent>(DestroyObjects)
        };
    }

    private void Start()
    {
        _parentTransform = GetComponentInParent<ActivatedItemsManager>().transform;
        OnBombDetonateEffect += StartDetonateBombEffect;
    }
    
    private void StartDetonateBombEffect()
    {
        var explosionFX = Instantiate(_explosionFX, _parentTransform);
        explosionFX.transform.position = transform.position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isBombActivated)
        {
            return;
        }
        if (other.gameObject.tag == GlobalConstants.CAHARACTER_TAG)
        {
            _isBombActivated = true;
            _bombRotation.StopRotation();
            EventStreams.Game.Publish(new BombCountdownStartEvent(_bombTimer, OnBombDetonateEffect));
        }
    }

    private void DestroyObjects(BombCountdownEndEvent eventData)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _bombExplosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.tag == GlobalConstants.CAHARACTER_TAG)
            {
                EventStreams.Game.Publish(new CharacterStateEvent(false));
            }
            if (collider.tag == GlobalConstants.ZOMBIE_TAG)
            {
                EventStreams.Game.Publish(new ZombieTakeDamageEvent(collider.gameObject, 10f));
            }
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnBombDetonateEffect -= StartDetonateBombEffect;
        _subscriptions?.Dispose();
    }
}