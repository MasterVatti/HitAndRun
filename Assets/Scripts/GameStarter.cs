using IngameStateMachine;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        var states = new IState[]
        {
            GetComponent<LobbyState>(),
            GetComponent<GameState>()
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<LobbyState>();
    }
}