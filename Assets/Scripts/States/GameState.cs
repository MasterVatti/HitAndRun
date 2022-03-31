using System.Collections;
using IngameStateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleEventBus.Disposables;

public class GameState : MonoBehaviour, IState
{
    [SerializeField] 
    private CharacterSettingsProvider _characterSettingsProvider;

    private CompositeDisposable _subscriptions;
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
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameOverEvent>(GoGameOverState),
            EventStreams.Game.Subscribe<GameWinEvent>(GoGameWinState)
            
        };
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
    
    private void GoGameOverState(GameOverEvent eventData)
    {
        _stateMachine.Enter<GameOverState>();
    }
    
    private void GoGameWinState(GameWinEvent eventData)
    {
        _stateMachine.Enter<GameWinState>();
    }

    public void OnExit()
    {
        _subscriptions?.Dispose();
    }
}