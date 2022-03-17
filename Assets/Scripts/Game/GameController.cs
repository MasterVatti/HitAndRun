using System;
using IngameStateMachine;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameController : MonoBehaviour
{
    private Movement _movement;
    private CameraController _cameraController;
    private ShootDirection _shootDirection;
    private BulletManager _bulletManager;
    
    public void StartGame(CharacterSettings character)
    {
        var newCharacter = Instantiate(character.Prefab);
        newCharacter.transform.position = new Vector3(10f, 0f, 103f);
        
        _movement = FindObjectOfType<Movement>();
        _cameraController = FindObjectOfType<CameraController>();
        _shootDirection = FindObjectOfType<ShootDirection>();
        _bulletManager = FindObjectOfType<BulletManager>();
        _movement.Initialize(newCharacter);
        _cameraController.Initialize(newCharacter);
        _shootDirection.Initialize(newCharacter);
        _bulletManager.Initialize(newCharacter);
    }
}