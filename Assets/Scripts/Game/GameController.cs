using System;
using IngameStateMachine;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameController : MonoBehaviour
{
    private MovementBehavior _movementBehavior;
    private CameraController _cameraController;
    
    public void StartGame(CharacterSettings character)
    {
        var newCharacter = Instantiate(character.Prefab);
        newCharacter.transform.position = new Vector3(10f, 0f, 103f);
        
        _movementBehavior = FindObjectOfType<MovementBehavior>();
        _cameraController = FindObjectOfType<CameraController>();
        _movementBehavior.Initialize(newCharacter);
        _cameraController.Initialize(newCharacter);
    }
}