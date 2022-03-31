using System;
using System.Linq;
using IngameStateMachine;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyState : MonoBehaviour, IState
{
    private CompositeDisposable _subscriptions;
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
            EventStreams.Game.Subscribe<StartGameEvent>(StartGameScene)
        };
        SceneManager.LoadScene(GlobalConstants.LOBBY_SCENE_NAME);
    }

    public void OnExit()
    {
        _subscriptions?.Dispose();
    }

    private void StartGameScene(StartGameEvent eventData)
    {
        _stateMachine.Enter<GameState>();
    }
}