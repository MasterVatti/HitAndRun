using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] 
    private GameObject _explosionFX;
    [SerializeField] 
    private ItemsRotation _bombRotation;
    [SerializeField]
    private int _bombTimer;

    [NonSerialized] 
    public Action OnBombDetonateEffect;
    
    private Transform _parentTransform;
    private bool _isBombActivated;

    private void Start()
    {
        _parentTransform = GetComponentInParent<BombManager>().transform;
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
            EventStreams.Game.Publish(new BombCountdownStartEvent(gameObject, _bombTimer, OnBombDetonateEffect));
        }
    }

    private void OnDestroy()
    {
        OnBombDetonateEffect -= StartDetonateBombEffect;
    }
}