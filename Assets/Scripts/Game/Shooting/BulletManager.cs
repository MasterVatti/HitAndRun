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
    private int _poolSize;
    [SerializeField] 
    private GameObject _muzzleFlare;

    private CompositeDisposable _subscriptions;
    private MonoBehaviourPool<Bullet> _bulletPool;
    private BulletShootPoint _bulletShootPoint;
    private GameObject _currentCharacter;
    private Animator _animator;
    private float _shootInterval;
    private float _startShootInterval;
    private float _animationShootingTime;
    private float _weaponDamageAmount;
    private float _bulletSpeed = 25f;

    private static readonly int _moving = Animator.StringToHash(GlobalConstants.CHARACTER_ANIMATOR_ISMOVING_PARAMETR);
    public bool IsMoving => _animator.GetBool(_moving);

    private void Awake()
    {
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
        if (_animationShootingTime > 0.35f)
        {
            _animator.Play(GlobalConstants.CHARACTER_ANIMATOR_STOP_SHOOTING_IDLE_ANIMATION);
            _animator.Play(GlobalConstants.CHARACTER_ANIMATOR_STOP_SHOOTING_RUN_ANIMATION);
        }
    }

    private void CharacterShootHandler(CharacterShotEvent eventData)
    {
        if (_startShootInterval <= 0)
        {
            var bullet = _bulletPool.Take();
            Instantiate(_muzzleFlare, _bulletShootPoint.transform);
            
            bullet.transform.position = _bulletShootPoint.transform.position;
            bullet.Initialize(_bulletShootPoint.transform.rotation, _bulletSpeed);
            SetShootingAnimation();
            _startShootInterval = _shootInterval;
            _animationShootingTime = 0;
        }
        else
        {
            _startShootInterval -= Time.deltaTime;
        }
    }

    private void SetShootingAnimation()
    {
        if (!IsMoving)
        {
            _animator.SetTrigger(GlobalConstants.CHARACTER_ANIMATOR_SHOOTING_IDLE_TRIGGER);
            _animator.Play(GlobalConstants.CHARACTER_ANIMATOR_STOP_SHOOTING_RUN_ANIMATION);
        }
        else
        {
            _animator.SetTrigger(GlobalConstants.CHARACTER_ANIMATOR_SHOOTING_RUN_TRIGGER);
            _animator.Play(GlobalConstants.CHARACTER_ANIMATOR_STOP_SHOOTING_IDLE_ANIMATION);
        }
    }

    private void BulletHitHandler(BulletHitEvent eventData)
    {
        if (eventData.HitObject.tag == GlobalConstants.ZOMBIE_TAG)
        {
            EventStreams.Game.Publish(new ZombieTakeDamageEvent(eventData.HitObject, _weaponDamageAmount));
        }
        var bullet = eventData.Bullet;
        _bulletPool.Release(bullet);
    }

    private void Initialize(CharacterInstantiatedEvent eventData)
    {
        _currentCharacter = eventData.Character;
        _bulletShootPoint = eventData.Character.GetComponentInChildren<BulletShootPoint>();
        _animator = _currentCharacter.GetComponent<Animator>();
    }

    public void SetWeaponCharacteristics(float damage, float rateFire)
    {
        _weaponDamageAmount = damage;
        _startShootInterval = 1f - rateFire;
        _shootInterval = _startShootInterval;
        _bulletSpeed = rateFire * 35f;
    }
    
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}