using System;
using SimpleEventBus.Disposables;
using UnityEngine;

public class ShootDirection : MonoBehaviour
{
    [SerializeField] 
    private FixedJoystick _joystick;
    
    private CompositeDisposable _subscriptions;
    private GameObject _currentCharacter;

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
        if (_currentCharacter == null)
        {
            return;
        }
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            var rotate = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
            _currentCharacter.transform.rotation = Quaternion.Euler(0, -rotate - 90f, 0);
            EventStreams.Game.Publish(new CharacterShotEvent(_currentCharacter.transform.rotation));
        }
    }
    
    private void DisableJoystick(GameOverLightChangeEvent eventData)
    {
        gameObject.SetActive(false);
    }
    
    private void Initialize(CharacterInstantiatedEvent eventData)
    {
        _currentCharacter = eventData.Character;
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}