using System;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] _bombEffects;
    [SerializeField]
    private float _bombExplosionRadius;

    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BombCountdownStartEvent>(DisableBombFX),
            EventStreams.Game.Subscribe<BombCountdownEndEvent>(DestroyObjects)
        };
    }
    
    private void DisableBombFX(BombCountdownStartEvent eventData)
    {
        if (eventData.Bomb != gameObject)
        {
            return;
        }
        foreach (var bombEffect in _bombEffects)
        {
            Destroy(bombEffect);
        }
    }

    private void DestroyObjects(BombCountdownEndEvent eventData)
    {
        if (eventData.Bomb != gameObject)
        {
           return; 
        }
        HashSet<GameObject> damagedObjects = new HashSet<GameObject>();
        int maxColliders = (int)Math.Truncate(_bombExplosionRadius * _bombExplosionRadius);
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, _bombExplosionRadius, hitColliders);
        
        FindObjectsNearby(damagedObjects, hitColliders, numColliders);
        Destroy(gameObject);
    }

    private void FindObjectsNearby(HashSet<GameObject> damagedObjects, Collider[] hitColliders, int numColliders)
    {
        for (int i = 0; i < numColliders; i++)
        {
            if (hitColliders[i].gameObject.tag == GlobalConstants.ZOMBIE_TAG && !damagedObjects.Contains(hitColliders[i].gameObject))
            {
                damagedObjects.Add(hitColliders[i].gameObject);
                EventStreams.Game.Publish(new ZombieTakeDamageEvent(hitColliders[i].gameObject, 100f));
            }
            if (hitColliders[i].tag == GlobalConstants.CAHARACTER_TAG && !damagedObjects.Contains(hitColliders[i].gameObject))
            {
                damagedObjects.Add(hitColliders[i].gameObject);
                EventStreams.Game.Publish(new CharacterTakeDamageEvent(0.7f));
            }
        }
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}