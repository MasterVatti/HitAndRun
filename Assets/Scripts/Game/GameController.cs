using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private CharacterSpawnController _characterSpawnController;
    [SerializeField] 
    private UIManager _uiManager;
    [SerializeField] 
    private ZombieManager _zombieManager;

    public void StartGame(CharacterSettings character)
    {
        _characterSpawnController.Initialize(character);
        _uiManager.Initialize(_zombieManager.GetAllZombiesQuantity());
    }
}