using UnityEngine;

public class CharacterSpawnController : MonoBehaviour
{
    [SerializeField] 
    private Transform _characterSpawnPoint;

    public void Initialize(CharacterSettings character)
    {
        var newCharacter = Instantiate(character.Prefab);
        newCharacter.transform.position = _characterSpawnPoint.position;
    }
}