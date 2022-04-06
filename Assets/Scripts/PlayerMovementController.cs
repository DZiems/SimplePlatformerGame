using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, IMove
{
    [Header("Running")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Jumping")]
    [SerializeField] private float _jumpForce = 500f;
    [SerializeField] private int _numAirJumps = 1;


    private float _horizontal;

    private int _remainingAirJumps;
    private bool _shouldJump;
    public event Action OnJump;


    private Rigidbody2D _rigidbody2D;
    private CharacterGrounding _characterGrounding;
    private Vector2 _startPosition;

    public float CurrentSpeed { get; private set; }

    public bool IsTravellingUpwards { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterGrounding = GetComponent<CharacterGrounding>();
        _startPosition = transform.position;

    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            if (_characterGrounding.IsGrounded)
            {
                _shouldJump = true;
            }
            else if (_remainingAirJumps > 0)
            {
                _remainingAirJumps--;
                _shouldJump = true;
            }
        }

        if (_characterGrounding.IsGrounded)
        {
            _remainingAirJumps = _numAirJumps;
        }
    }


    private void FixedUpdate()
    {
        _horizontal = Input.GetAxis("Horizontal");
        CurrentSpeed = _horizontal;

        Vector3 movement = new Vector3(_horizontal, 0f);
        transform.position += movement * Time.deltaTime * _moveSpeed;

        if (_shouldJump)
        {
            Jump();
        }

    }

    internal void ResetPosition()
    {
        transform.position = _startPosition;
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
        _shouldJump = false;

        if (_characterGrounding.GroundedDirection != Vector2.down)
        {

            _rigidbody2D.AddForce(_characterGrounding.GroundedDirection * -1f * _jumpForce);
        }

        if (OnJump != null)
            OnJump();
    }

    internal void Bounce()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }
}
