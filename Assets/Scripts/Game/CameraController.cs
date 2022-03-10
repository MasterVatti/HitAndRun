using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _currentCharacter;
    private Vector3 _target;
    [SerializeField] 
    private float _cameraPositionSpeed;
    [SerializeField]
    private float _differenceCharacterAndCameraPositionZ;

    private void LateUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        _target = new Vector3(_currentCharacter.transform.position.x, transform.position.y, _currentCharacter.transform.position.z + _differenceCharacterAndCameraPositionZ);
        Vector3 currentPosition = Vector3.Lerp(transform.position, _target, _cameraPositionSpeed * Time.deltaTime);
        transform.position = currentPosition;
    }
    
    public void Initialize(GameObject character)
    {
        _currentCharacter = character;
    }
}