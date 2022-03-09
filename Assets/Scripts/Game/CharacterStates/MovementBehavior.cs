using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementBehavior : MonoBehaviour
{
    [SerializeField] 
    private FixedJoystick _joystick;
    [SerializeField] 
    private float _moveSpeed;
    
    private Rigidbody _characterRigidbody;
    private GameObject _currentCharacter;
    private Animator _animator;
    private bool _isCharacterInstantiate;
    private static readonly int _moving = Animator.StringToHash(GlobalConstants.CHARACTER_ANIMATOR_ISMOVING_PARAMETR);

    public bool IsMoving
    {
        get => _animator.GetBool(_moving);
        set => _animator.SetBool(_moving, value);
    }

    private void FixedUpdate()
    {
        if (!_isCharacterInstantiate)
        {
            return;
        }
        MovementLogic();
    }

    private void Awake()
    {
        _isCharacterInstantiate = false;
    }

    private void MovementLogic()
    {
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

    public void Initialize(GameObject character)
    {
        _currentCharacter = character;
        _characterRigidbody = _currentCharacter.GetComponent<Rigidbody>();
        _animator = _currentCharacter.GetComponent<Animator>();
        _isCharacterInstantiate = true;
    }
}