using IngameStateMachine;
using UnityEngine;
using SimpleEventBus.Disposables;

public class GameOverState : MonoBehaviour, IState
{
    private CompositeDisposable _subscriptions;
    private StateMachine _stateMachine;
    private GameController _gameController;
    
    public void Dispose()
    {
        
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _gameController = FindObjectOfType<GameController>();
        _gameController.GetGameOverGUI().gameObject.SetActive(true);
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<StartGameEvent>(StartLobbyScene)
        };
    }

    private void StartLobbyScene(StartGameEvent eventData)
    {
        _stateMachine.Enter<LobbyState>();
    }

    public void OnExit()
    {
        _subscriptions?.Dispose();
    }
    
}