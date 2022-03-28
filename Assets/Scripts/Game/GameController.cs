using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private CharacterController characterController;
    [SerializeField] 
    private UIManager _uiManager;
    [SerializeField] 
    private ZombieManager _zombieManager;
    [SerializeField] 
    private Canvas _gameOverGUI;

    public void StartGame(CharacterSettings character)
    {
        characterController.Initialize(character);
        _uiManager.Initialize(_zombieManager.GetAllZombiesQuantity(), character);
    }

    public Canvas GetGameOverGUI()
    {
        return _gameOverGUI;
    }
}