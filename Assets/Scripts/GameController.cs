using System;
using IngameStateMachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movementBehavior;
    

    public void StartGame(CharacterSettings character)
    {
        var newCharacter = Instantiate(character.Prefab);
        newCharacter.transform.position = new Vector3(18f, 0, 30f);
        _movementBehavior.Initialize(newCharacter);
        
    }


}