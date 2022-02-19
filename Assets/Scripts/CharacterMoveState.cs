using System;
using IngameStateMachine;
using UnityEngine;

public class CharacterMoveState : MonoBehaviour, IState
{
    private StateMachine _stateMachine;
    private MovementBehavior _movementBehavior;
    
    public void Dispose()
    {
        
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _movementBehavior = FindObjectOfType<MovementBehavior>();
        //_movementBehavior.Moving(true);
    }

    public void OnExit()
    {

    }

    private void FixedUpdate()
    {
        if (_movementBehavior == null)
        {
            return;
        }
        if (_movementBehavior.IdleTime >= 1f)
        {
            _stateMachine.Enter<CharacterWaitingState>();
        }
    }
}