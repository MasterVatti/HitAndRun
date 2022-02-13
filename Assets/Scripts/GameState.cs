using IngameStateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour, IState
{
    [SerializeField] 
    private CharacterSettingsProvider _characterSettingsProvider;

    private GameController _gameController;
    
    public void Dispose()
    {
        
    }

    public void Initialize(StateMachine stateMachine)
    {
        
    }

    public void OnEnter()
    {
        SceneManager.LoadScene(GlobalConstants.GAME_SCENE_NAME);
        _gameController = FindObjectOfType<GameController>();
        var lastSelectedCharacterName = PrefsManager.GetLastSelectedCharacter();
        var lastSelectedCharacter = _characterSettingsProvider.GetCharacter(lastSelectedCharacterName);
        
        _gameController.StartGame(lastSelectedCharacter);
    }

    public void OnExit()
    {
        
    }
}