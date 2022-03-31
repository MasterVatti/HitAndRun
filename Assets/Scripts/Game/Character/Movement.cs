using UnityEngine;
using SimpleEventBus.Disposables;
using UnityEditor;

public class Movement : MonoBehaviour
{
    [SerializeField] 
    private FixedJoystick _joystick;

    private CompositeDisposable _subscriptions;
    private Rigidbody _characterRigidbody;
    private GameObject _currentCharacter;
    private Animator _animator;
    private float _moveSpeed;
    
    private static readonly int _moving = Animator.StringToHash(GlobalConstants.CHARACTER_ANIMATOR_ISMOVING_PARAMETR);
    public bool IsMoving
    {
        get => _animator.GetBool(_moving);
        set => _animator.SetBool(_moving, value);
    }

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterInstantiatedEvent>(Initialize),
            EventStreams.Game.Subscribe<GameOverLightChangeEvent>(DisableJoystick)
        };
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        if (_currentCharacter == null || _characterRigidbody == null)
        {
           return; 
        }
        _characterRigidbody.velocity = new Vector3(-_joystick.Horizontal * _moveSpeed, _characterRigidbody.velocity.y, -_joystick.Vertical * _moveSpeed);
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _currentCharacter.transform.rotation = Quaternion.LookRotation(_characterRigidbody.velocity);
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }
    
    private void DisableJoystick(GameOverLightChangeEvent eventData)
    {
        gameObject.SetActive(false);
    }
    
    private void Initialize(CharacterInstantiatedEvent eventData)
    {
        _currentCharacter = eventData.Character;
        _characterRigidbody = eventData.Character.GetComponent<Rigidbody>();
        _animator = eventData.Character.GetComponent<Animator>();
    }
    
    public void SetCharacterSpeed(float speed)
    {
        _moveSpeed = speed * 10f;
    }
        
    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}