using UnityEngine;

public class GameController : MonoBehaviour
{
    public void StartGame(CharacterSettings character)
    {
        Instantiate(character.Prefab);
    }
}