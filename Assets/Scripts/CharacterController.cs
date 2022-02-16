using IngameStateMachine;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        var states = new IState[]
        {
            GetComponent<CharacterWaitingState>(),
            GetComponent<CharacterMoveState>()
        };

        _stateMachine = new StateMachine(states);
        _stateMachine.Initialize();
        _stateMachine.Enter<CharacterWaitingState>();
    }
}