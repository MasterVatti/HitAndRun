using System;
using IngameStateMachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private MovementBehavior _movementBehavior;
    
    public void StartGame(CharacterSettings character)
    {
        var newCharacter = Instantiate(character.Prefab);
        newCharacter.transform.position = new Vector3(10f, 0f, 103f);
        _movementBehavior = FindObjectOfType<MovementBehavior>();
        _movementBehavior.Initialize(newCharacter);
    }


}