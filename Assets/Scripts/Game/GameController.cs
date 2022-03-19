using UnityEngine;

public class GameController : MonoBehaviour
{
    public void StartGame(CharacterSettings character)
    {
        var newCharacter = Instantiate(character.Prefab);
        newCharacter.transform.position = new Vector3(10f, 0f, 103f);
    }
}