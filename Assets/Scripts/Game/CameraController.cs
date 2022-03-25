using System;
using UnityEngine;
using SimpleEventBus.Disposables;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private float _cameraPositionSpeed;
    [SerializeField]
    private float _differenceCharacterAndCameraPositionZ;

    private CompositeDisposable _subscriptions;
    private GameObject _currentCharacter;
    private Vector3 _target;
    
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<CharacterInstantiatedEvent>(Initialize)
        };
    }
    
    private void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        if (_currentCharacter == null)
        {
            return;
        }
        _target = new Vector3(_currentCharacter.transform.position.x, transform.position.y, _currentCharacter.transform.position.z + _differenceCharacterAndCameraPositionZ);
        Vector3 currentPosition = Vector3.Lerp(transform.position, _target, _cameraPositionSpeed * Time.deltaTime);
        transform.position = currentPosition;
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