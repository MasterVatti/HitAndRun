using System;
using System.Collections;
using Pools;
using SimpleEventBus.Disposables;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] 
    private Bullet _bulletPrefab;
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private int _poolSize = 30;
    [SerializeField]
    private float _shootInterval = 0.2f;
    [SerializeField] 
    private GameObject _muzzleFlare;

    private CompositeDisposable _subscriptions;
    private MonoBehaviourPool<Bullet> _bulletPool;
    private BulletShootPoint _bulletShootPoint;
    private GameObject _currentCharacter;
    private Animator _animator;
    private float _startShootInterval;
    private float _animationShootingTime;
    
    private void Awake()
    {
        _startShootInterval = _shootInterval;
        _bulletPool = new MonoBehaviourPool<Bullet>(_bulletPrefab, _bulletSpawnPoint, _poolSize);

        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterShotEvent>(CharacterShootHandler),
            EventStreams.Game.Subscribe<BulletHitEvent>(BulletHitHandler),
            EventStreams.Game.Subscribe<CharacterInstantiatedEvent>(Initialize)
        };
    }

    private void FixedUpdate()
    {
        _animationShootingTime += Time.deltaTime;
        if (_animationShootingTime > 0.6f)
        {
            _animator.Play(GlobalConstants.CHARACTER_ANIMATOR_STOP_SHOOTING_ANIMATION);
        }
    }

    private void CharacterShootHandler(CharacterShotEvent eventData)
    {
        Shoot(eventData);
    }

    private void Shoot(CharacterShotEvent eventData)
    {
        if (_startShootInterval <= 0)
        {
            var bullet = _bulletPool.Take();
            Instantiate(_muzzleFlare, _bulletShootPoint.transform);
            
            bullet.transform.position = _bulletShootPoint.transform.position;
            bullet.SetBulletRotation(eventData.CharacterTransformRotation);
            _startShootInterval = _shootInterval;
            _animator.SetTrigger(GlobalConstants.CHARACTER_ANIMATOR_SHOOTING_TRIGGER);
            _animationShootingTime = 0;
        }
        else
        {
            _startShootInterval -= Time.deltaTime;
        }
    }
    
    private void BulletHitHandler(BulletHitEvent eventData)
    {
        var bullet = eventData.Bullet;
        _bulletPool.Release(bullet);
    }

    private void Initialize(CharacterInstantiatedEvent eventData)
    {
        _currentCharacter = eventData.Character;
        _bulletShootPoint = eventData.Character.GetComponentInChildren<BulletShootPoint>();
        _animator = _currentCharacter.GetComponent<Animator>();
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}