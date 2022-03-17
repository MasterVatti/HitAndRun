using System;
using SimpleEventBus.Disposables;
using UnityEngine;
using System.Collections;

public class ShootDirection : MonoBehaviour
{
    [SerializeField] 
    private FixedJoystick _joystick;
    
    private CompositeDisposable _subscriptions;
    private GameObject _currentCharacter;

    private void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            var rotate = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
            _currentCharacter.transform.rotation = Quaternion.Euler(0, -rotate - 90f, 0);
            EventStreams.Game.Publish(new CharacterShotEvent(_currentCharacter.transform.rotation));
        }
    }

    public void Initialize(GameObject character)
    {
        _currentCharacter = character;
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}