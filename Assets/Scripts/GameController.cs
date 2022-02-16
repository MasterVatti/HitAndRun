using System;
using IngameStateMachine;
using UnityEngine;

public class CharacterMoveState : MonoBehaviour, IState
{
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
        
    }

    public void OnExit()
    {
        
    }
}

public class CharacterWaitingState : MonoBehaviour, IState
{
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
        
    }

    public void OnExit()
    {
        
    }
}

public class GameController : MonoBehaviour
{
    private Rigidbody _characterRigidbody;
    private float _speed = 60f;
    private float _jumpForce = 400f;
    private bool _isGrounded;

    public void StartGame(CharacterSettings character)
    {
        var temp = Instantiate(character.Prefab);
        _characterRigidbody = temp.GetComponent<Rigidbody>();
        temp.transform.position = new Vector3(18f, 0, 30f);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
        
    }
    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        _characterRigidbody.AddForce(movement * _speed);
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                _characterRigidbody.AddForce(Vector3.up * _jumpForce);

                // Обратите внимание что я делаю на основе Vector3.up 
                // а не на основе transform.up. Если персонаж упал или 
                // если персонаж -- шар, то его личный "верх" может 
                // любое направление. Влево, вправо, вниз...
                // Но нам нужен скачек только в абсолютный вверх, 
                // потому и Vector3.up
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
}