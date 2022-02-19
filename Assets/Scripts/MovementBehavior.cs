using System;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    private Rigidbody _characterRigidbody;
    private float _speed = 60f;
    private float _jumpForce = 400f;
    private bool _isGrounded;
    private GameObject _currentCharacter;
    private bool _isMoving;
    [NonSerialized] public float IdleTime;
    
    private void Update()
    {
        
    }

    private void Awake()
    {

    }
    
    
    private void FixedUpdate()
    {
        if (!_isMoving)
        {
            return;
        }
        var startPosition = _characterRigidbody.position;
        MovementLogic();
        JumpLogic();
        IdleTime = _characterRigidbody.position == startPosition ? Time.time : 0f;
        
    }

    public void Moving(bool isMoving)
    {
        _isMoving = isMoving;
    }
    
    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        _characterRigidbody.AddForce(moveHorizontal * _speed, 0.0f, moveVertical * _speed);
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                _characterRigidbody.AddForce(0,transform.position.y * _jumpForce,0);

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

    public void Initialize(GameObject character)
    {
        _currentCharacter = character;
        _characterRigidbody = _currentCharacter.GetComponent<Rigidbody>();
    }
}