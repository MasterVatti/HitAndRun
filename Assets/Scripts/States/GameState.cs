using System.Collections;
using IngameStateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour, IState
{
    [SerializeField] 
    private CharacterSettingsProvider _characterSettingsProvider;

    private GameController _gameController;
    private StateMachine _stateMachine;
    
    public void Dispose()
    {
        
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        SceneManager.LoadScene(GlobalConstants.GAME_SCENE_NAME);
        yield return null;
        _gameController = FindObjectOfType<GameController>();
        var lastSelectedCharacterName = PrefsManager.GetLastSelectedCharacter();
        var lastSelectedCharacter = _characterSettingsProvider.GetCharacter(lastSelectedCharacterName);
        _gameController.StartGame(lastSelectedCharacter);
    }

    public void OnExit()
    {
        
    }
}