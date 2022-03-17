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
    private SpawnBullet _spawnBullet;
    private GameObject _currentCharacter;
    private Animator _animator;
    private bool _isCharacterInstantiate;
    private float _startShootInterval;
    private float _animationShootingTime;
    
    private void Awake()
    {
        _isCharacterInstantiate = false;
        _startShootInterval = _shootInterval;
        _bulletPool = new MonoBehaviourPool<Bullet>(_bulletPrefab, _bulletSpawnPoint, _poolSize);

        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterShotEvent>(CharacterShootHandler),
            EventStreams.Game.Subscribe<BulletHitEvent>(BulletHitHandler)
        };
    }

    private void FixedUpdate()
    {
        if (!_isCharacterInstantiate)
        {
            return;
        }
        _animationShootingTime += Time.deltaTime;
        if (_animationShootingTime > 0.6f)
        {
            _animator.Play(GlobalConstants.CHARACTER_ANIMATOR_STOP_SHOOTING_ANIMATION);
        }
    }

    private void CharacterShootHandler(CharacterShotEvent eventData)
    {
        if (!_isCharacterInstantiate)
        {
            return;
        }
        Shoot(eventData);
    }

    private void Shoot(CharacterShotEvent eventData)
    {
        if (_startShootInterval <= 0)
        {
            var bullet = _bulletPool.Take();
            Instantiate(_muzzleFlare, _spawnBullet.transform);
            
            bullet.transform.position = _spawnBullet.transform.position;
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

    public void Initialize(GameObject character)
    {
        _currentCharacter = character;
        _spawnBullet = character.GetComponentInChildren<SpawnBullet>();
        _animator = _currentCharacter.GetComponent<Animator>();
        _isCharacterInstantiate = true;
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}